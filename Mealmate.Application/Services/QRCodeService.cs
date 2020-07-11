using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using Mealmate.Core.Interfaces;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Paging;

namespace Mealmate.Application.Services
{
    public class QRCodeService : IQRCodeService
    {
        private readonly IQRCodeRepository _qrCodeRepository;
        private readonly IAppLogger<QRCodeService> _logger;
        private readonly IMapper _mapper;

        public QRCodeService(
            IQRCodeRepository qrCodeRepository, 
            IAppLogger<QRCodeService> logger, 
            IMapper mapper)
        {
            _qrCodeRepository = qrCodeRepository ?? throw new ArgumentNullException(nameof(qrCodeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<QRCodeModel> Create(QRCodeModel model)
        {
            var existingQRCode = await _qrCodeRepository.GetByIdAsync(model.Id);
            if (existingQRCode != null)
            {
                throw new ApplicationException("qrCode with this id already exists");
            }

            var newqrCode = _mapper.Map<QRCode>(model);
            newqrCode = await _qrCodeRepository.SaveAsync(newqrCode);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newqrCodemodel = _mapper.Map<QRCodeModel>(newqrCode);
            return newqrCodemodel;
        }

        public async Task Delete(int id)
        {
            var existingQRCode = await _qrCodeRepository.GetByIdAsync(id);
            if (existingQRCode == null)
            {
                throw new ApplicationException("QRCode with this id is not exists");
            }

            await _qrCodeRepository.DeleteAsync(existingQRCode);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<QRCodeModel>> Get(int tableId)
        {
            var result = await _qrCodeRepository.GetAsync(x => x.TableId== tableId);
            return _mapper.Map<IEnumerable<QRCodeModel>>(result);
        }

        public async Task<QRCodeModel> GetById(int id)
        {
            return _mapper.Map<QRCodeModel>(await _qrCodeRepository.GetByIdAsync(id));
        }

        public async Task Update(QRCodeModel model)
        {
            var existingQRCode = await _qrCodeRepository.GetByIdAsync(model.Id);
            if (existingQRCode == null)
            {
                throw new ApplicationException("QRCode with this id is not exists");
            }

            existingQRCode = _mapper.Map<QRCode>(model);

            await _qrCodeRepository.SaveAsync(existingQRCode);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        //public async Task<IEnumerable<QRCodeModel>> GetQRCodeList()
        //{
        //    var QRCodeList = await _qrCodeRepository.ListAllAsync();

        //    var QRCodeModels = ObjectMapper.Mapper.Map<IEnumerable<QRCodeModel>>(QRCodeList);

        //    return QRCodeModels;
        //}

        //public async Task<IPagedList<QRCodeModel>> SearchQRCodes(PageSearchArgs args)
        //{
        //    var QRCodePagedList = await _qrCodeRepository.SearchQRCodesAsync(args);

        //    //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
        //    var QRCodeModels = ObjectMapper.Mapper.Map<List<QRCodeModel>>(QRCodePagedList.Items);

        //    var QRCodeModelPagedList = new PagedList<QRCodeModel>(
        //        QRCodePagedList.PageIndex,
        //        QRCodePagedList.PageSize,
        //        QRCodePagedList.TotalCount,
        //        QRCodePagedList.TotalPages,
        //        QRCodeModels);

        //    return QRCodeModelPagedList;
        //}

        //public async Task<QRCodeModel> GetQRCodeById(int QRCodeId)
        //{
        //    var QRCode = await _qrCodeRepository.GetByIdAsync(QRCodeId);

        //    var QRCodeModel = ObjectMapper.Mapper.Map<QRCodeModel>(QRCode);

        //    return QRCodeModel;
        //}

        //public async Task<IEnumerable<QRCodeModel>> GetQRCodesByName(string name)
        //{
        //    var spec = new QRCodeWithQRCodeesSpecification(name);
        //    var QRCodeList = await _qrCodeRepository.GetAsync(spec);

        //    var QRCodeModels = ObjectMapper.Mapper.Map<IEnumerable<QRCodeModel>>(QRCodeList);

        //    return QRCodeModels;
        //}

        //public async Task<IEnumerable<QRCodeModel>> GetQRCodesByCategoryId(int categoryId)
        //{
        //    var spec = new QRCodeWithQRCodeesSpecification(categoryId);
        //    var QRCodeList = await _qrCodeRepository.GetAsync(spec);

        //    var QRCodeModels = ObjectMapper.Mapper.Map<IEnumerable<QRCodeModel>>(QRCodeList);

        //    return QRCodeModels;
        //}

        //public async Task<QRCodeModel> CreateQRCode(QRCodeModel QRCode)
        //{
        //    var existingQRCode = await _qrCodeRepository.GetByIdAsync(QRCode.Id);
        //    if (existingQRCode != null)
        //    {
        //        throw new ApplicationException("QRCode with this id already exists");
        //    }

        //    var newQRCode = ObjectMapper.Mapper.Map<QRCode>(QRCode);
        //    newQRCode = await _qrCodeRepository.SaveAsync(newQRCode);

        //    _logger.LogInformation("Entity successfully added - MealmateAppService");

        //    var newQRCodeModel = ObjectMapper.Mapper.Map<QRCodeModel>(newQRCode);
        //    return newQRCodeModel;
        //}

        //public async Task UpdateQRCode(QRCodeModel QRCode)
        //{
        //    var existingQRCode = await _qrCodeRepository.GetByIdAsync(QRCode.Id);
        //    if (existingQRCode == null)
        //    {
        //        throw new ApplicationException("QRCode with this id is not exists");
        //    }

        //    existingQRCode.Name = QRCode.Name;
        //    existingQRCode.Description = QRCode.Description;

        //    await _qrCodeRepository.SaveAsync(existingQRCode);

        //    _logger.LogInformation("Entity successfully updated - MealmateAppService");
        //}

        //public async Task DeleteQRCodeById(int QRCodeId)
        //{
        //    var existingQRCode = await _qrCodeRepository.GetByIdAsync(QRCodeId);
        //    if (existingQRCode == null)
        //    {
        //        throw new ApplicationException("QRCode with this id is not exists");
        //    }

        //    await _qrCodeRepository.DeleteAsync(existingQRCode);

        //    _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        //}
    }
}
