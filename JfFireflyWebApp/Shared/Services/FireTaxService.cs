using FireflyIII;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace JfFireflyWebApp.Shared.Services
{
    public class FireTaxService : IFireTaxService
    {
        private readonly AppSettings _appSettings;
        private readonly Client _client;

        public FireTaxService(AppSettings appSettings)
        {
            _appSettings = appSettings;
            _client = new Client(_appSettings.FireflyConfig);
        }

        public async Task<List<(TransactionSplit ts, string desc)>> GetExpensesAccountsAsync(int searchId)
        {
            var result = new List<(TransactionSplit ts, string desc)>();

            var pew = await _client.ListTransactionByAccountAsync(searchId, null, null, null, null, null);

            foreach (var transaction in pew.Data)
            {
                foreach (var ta in transaction.Attributes.Transactions)
                {
                    var description = $"{ta.Destination_id} - {ta.Destination_name}";
                    if (ta.Source_id == searchId &&
                        ta.Destination_type == AccountTypeProperty.Expense_account &&
                        !result.Any(x => x.desc == description))
                    {
                        result.Add((ta, description));
                    }
                }
            }

            return result;
        }

        public async Task<SystemInfo> GetSystemInfoAsync() => await _client.GetAboutAsync();

        public async Task<TaxmanResult> GetTaxmanResultAsync(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var tagUrlTemplate = _appSettings.FireflyConfig.BaseUrl + _appSettings.FireflyConfig.FireflyTagShowUrlPart;

            var result = new TaxmanResult
            {
                FromDate = startDate,
                ToDate = endDate,
                Mappings = _appSettings.TaxmanMappings,
            };

            var culture = CultureInfo.CreateSpecificCulture("en-US");

            foreach (var m in result.Mappings)
            {
                var sr = new TaxmanTagSearchResult
                {
                    MappingToSearch = m,
                };

                foreach (var tag in m.FireflyTags)
                {
                    var tagResult = new TaxmanTagSearchResult.TagResult
                    {
                        Tag = tag,
                    };

                    TagSingle tagSingle;

                    try
                    {
                        tagSingle = await _client.GetTagAsync(tag, null);
                        tagResult.TagId = tagSingle.Data.Id;
                        tagResult.FireflyUrl = string.Format(tagUrlTemplate, tagSingle.Data.Id);
                    }
                    catch (Exception ex)
                    {
                        sr.Note = $"Error getting Tag \"{tag}\"\n{ex.Message}";
                        sr.TagSubResults.Add(tagResult);
                        continue;
                    }

                    try
                    {
                        var v = await _client.ListTransactionByTagAsync(tag, null, result.FromDate, result.ToDate, null);

                        foreach (var d in v.Data)
                        {
                            foreach (var ts in d.Attributes.Transactions)
                            {
                                tagResult.Subvalue += float.Parse(ts.Amount, NumberStyles.Any, culture);
                            }
                        }

                        sr.Amount += tagResult.Subvalue;
                    }
                    catch (Exception ex)
                    {
                        sr.Note = $"Error summarizing transactions: {ex.Message}";
                    }

                    sr.TagSubResults.Add(tagResult);
                }

                result.SearchResults.Add(sr);
            }

            return result;
        }
    }
}
