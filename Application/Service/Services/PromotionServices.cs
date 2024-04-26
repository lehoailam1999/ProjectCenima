using Application.Payload.Converter;
using Application.Payload.DataRequest;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using Application.Service.IServices;
using Domain.Entities;
using Domain.InterfaceRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Services
{
    public class PromotionServices : IPromotionServices
    {
        private readonly IBaseRepositories<Promotion> _basePromotionRepositories;
        private readonly Converter_Promotion _converter;
        private readonly ResponseObject<Response_Promotion> _respon;
        private readonly ResponseObject<List<Response_Promotion>> _responList;
        private readonly Response_Pagination<Response_Promotion> _listRespagination;

        public PromotionServices(IBaseRepositories<Promotion> basePromotionRepositories, Converter_Promotion converter, ResponseObject<Response_Promotion> respon, ResponseObject<List<Response_Promotion>> responList, Response_Pagination<Response_Promotion> listRespagination)
        {
            _basePromotionRepositories = basePromotionRepositories;
            _converter = converter;
            _respon = respon;
            _responList = responList;
            _listRespagination = listRespagination;
        }

        public async Task<ResponseObject<Response_Promotion>> AddNewPromotion(Request_Promotion request)
        {
            Promotion promotion = new Promotion();
            promotion.Percent = request.Percent;
            promotion.Description = request.Description;
            promotion.Quantity = request.Quantity;
            promotion.Type = request.Type;
            promotion.StartTime = request.StartTime;
            promotion.EndTime =   request.EndTime;
            promotion.Name = request.Name;
            promotion.IsActive = true;
            promotion.RankCustomerId = request.RankCustomerId;
            await _basePromotionRepositories.AddAsync(promotion);
            return _respon.ResponseSuccess("Add food Successfully", _converter.EntityToDTO(promotion));
        }

        public async Task<string> DeletePromotion(int id)
        {
            var promotion = await _basePromotionRepositories.FindAsync(id);
            if (promotion == null)
            {
                return "NotFound";
            }
            promotion.IsActive = false;
            await _basePromotionRepositories.UpdateAsync(promotion);
            return "Delete Food Successfully";
        }

        public async Task<ResponseObject<List<Response_Promotion>>> GetAll()
        {
            var list = await _basePromotionRepositories.GetAll();
            return _responList.ResponseSuccess("Danh sach Food", _converter.EntityToListDTO(list));
        }

        public async Task<ResponseObject<Response_Promotion>> UpdatePromotion(int id, Request_Promotion request)
        {
            var promotion = await _basePromotionRepositories.FindAsync(id);
            if(promotion == null)
            {
                 return _respon.ResponseError(StatusCodes.Status404NotFound,"Not Found",null);
            }
            promotion.Percent = request.Percent;
            promotion.Description = request.Description;
            promotion.Quantity = request.Quantity;
            promotion.Type = request.Type;
            promotion.StartTime = request.StartTime;
            promotion.EndTime = request.EndTime;
            promotion.Name = request.Name;
            promotion.RankCustomerId = request.RankCustomerId;
            await _basePromotionRepositories.UpdateAsync(promotion);
            return _respon.ResponseSuccess( "Cap nhat thanh cong", _converter.EntityToDTO(promotion));

        }
    }
}
