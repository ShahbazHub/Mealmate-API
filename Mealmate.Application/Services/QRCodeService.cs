using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

using QRCoder;

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

        public async Task<QRCodeModel> Create(QRCodeCreateModel model)
        {

            var newqrCode = new Mealmate.Core.Entities.QRCode
            {
                Created = DateTime.Now,
                TableId = model.TableId

            };

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
            var result = await _qrCodeRepository.GetAsync(x => x.TableId == tableId);
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

            existingQRCode = _mapper.Map<Core.Entities.QRCode>(model);

            await _qrCodeRepository.SaveAsync(existingQRCode);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        private byte[] GenerateQRCode(string textToEncode)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(textToEncode, QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            return BitmapToBytes(qrCodeImage);
        }
        private static byte[] BitmapToBytes(Bitmap img)
        {
            using MemoryStream stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
