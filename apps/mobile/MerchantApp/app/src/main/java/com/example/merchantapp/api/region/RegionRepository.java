package com.example.merchantapp.api.region;

import com.example.merchantapp.api.merchant.MerchantApiService;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.region.RegionItem;
import com.example.merchantapp.service.ApiClient;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;

public class RegionRepository {
    private final RegionApiService regionApi;
    public RegionRepository(){
        this.regionApi = ApiClient.getRegionService();
    }
    public void getProvinces(Callback<ApiResponse<List<RegionItem>>> callback){
        regionApi.getProvinces().enqueue(callback);
    }
    public void getCities(Integer id, Callback<ApiResponse<List<RegionItem>>> callback){
        regionApi.getCities(id).enqueue(callback);
    }
    public void getDistricts(Integer id, Callback<ApiResponse<List<RegionItem>>> callback){
        regionApi.getDistricts(id).enqueue(callback);
    }
}
