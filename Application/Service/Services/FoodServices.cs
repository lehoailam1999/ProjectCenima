﻿using Application.Payload.Converter;
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
    public class FoodServices : IFoodServices
    {
        private readonly IBaseRepositories<Food> _baseFoodRepositories;
        private readonly Converter_Food _converter;
        private readonly ResponseObject<Response_Food> _respon;
        private readonly Response_Pagination<Response_Food> _listRes;
        private readonly IPhotoServices _photoServices;

        public FoodServices(IBaseRepositories<Food> baseFoodRepositories, Converter_Food converter, ResponseObject<Response_Food> respon, Response_Pagination<Response_Food> listRes, IPhotoServices photoServices)
        {
            _baseFoodRepositories = baseFoodRepositories;
            _converter = converter;
            _respon = respon;
            _listRes = listRes;
            _photoServices = photoServices;
        }

        public async Task<ResponseObject<Response_Food>> AddNewFood(Request_Food request)
        {
            Food food = new Food();
            food.Price = request.Price;
            food.NameofFood = request.NameofFood;
            food.IsActive = true;
            food.Description = request.Description; 
            food.Image = await _photoServices.UploadPhoToAsync(request.Image);
            await _baseFoodRepositories.AddAsync(food);
            return _respon.ResponseSuccess("Add food Successfully", _converter.EntityToDTO(food));
        }

        public async Task<string> DeleteFood(int id)
        {
            var foodDelete = await _baseFoodRepositories.FindAsync(id);
            if (foodDelete == null)
            {
                return "NotFound";
            }
            foodDelete.IsActive = false;
            await _baseFoodRepositories.UpdateAsync(foodDelete);
            return "Delete Food Successfully";
        }

        public async Task<Response_Pagination<Response_Food>> GetAll(int pageSize,int pageNumber)
        {
            var list = await _baseFoodRepositories.GetAll();
            return _listRes.ResponseSuccess("Danh sach Food",pageSize,pageNumber, _converter.EntityToListDTO(list));
        }

        public async Task<ResponseObject<Response_Food>> UpdateFood(int id)
        {
            var foodUpdate = await _baseFoodRepositories.FindAsync(id);
            if (foodUpdate == null)
            {
                return _respon.ResponseError(StatusCodes.Status404NotFound, "Khong tim thay cinema", null);
            }
            await _baseFoodRepositories.UpdateAsync(foodUpdate);
            return _respon.ResponseSuccess("Update Food Successfully", _converter.EntityToDTO(foodUpdate));
        }
    }
}
