using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface ICurrencyConvertionService
    {
        ResultModel ConvertAnyCurrencyToAnyCurrency(CurrencyConverterDTO model);
        List<ValidationResult> AddRate(Rate model);
    }

    public class CurrencyConvertionService : ICurrencyConvertionService
    {
        private DataContext _dataContext;
        public CurrencyConvertionService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Rate> GetAllRates()
        {
            return _dataContext.Rates;
        }

        public Rate GetRateById(int Id)
        {
            return GetAllRates().Where(x => x.Id == Id).FirstOrDefault();
        }

        public List<ValidationResult> AddRate(Rate model)
        {
            var errorlist = new List<ValidationResult> { };
            try
            {

                _dataContext.Rates.Add(model);
                _dataContext.SaveChanges();
            }
            catch(Exception ex)
            {
                errorlist.Add(new ValidationResult(ex.Message));
                return errorlist;
            }
            return errorlist;
        }

        public ResultModel ConvertAnyCurrencyToAnyCurrency(CurrencyConverterDTO model)
        {
            var errorlist = new List<ValidationResult> { };
            var resultModel = new ResultModel();
            if(model.Amount == 0)
            {
                resultModel.errorList.Add(new ValidationResult("Amount cannot be Zero"));
                return resultModel;
            }

            if(model.FromCurrency <= 0)
            {
                resultModel.errorList.Add(new ValidationResult("Please select a currency"));
                return resultModel;
            }

            if (model.ToCurrency <= 0)
            {
                resultModel.errorList.Add(new ValidationResult("Please select a currency"));
                return resultModel;
            }

            var currency = GetAllRates().Where(x => x.FromCurrency == model.FromCurrency && x.ToCurrency == model.ToCurrency).FirstOrDefault();
            if(currency is null)
            {
                resultModel.errorList.Add(new ValidationResult("No currency value found"));
                return resultModel;
            }
            var rate = currency.Percentage;
            var newAmount = model.Amount * rate;
            resultModel.newAmount = newAmount;
            return resultModel;

        }
    }
}
