package com.example.merchantapp.api.region;

import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.region.RegionItem;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Query;

public interface RegionApiService {
    @GET("/api/regions/provinces")
    Call<ApiResponse<List<RegionItem>>> getProvinces();
    @GET("api/regions/cities")
    Call<ApiResponse<List<RegionItem>>> getCities(
            @Query("provinceId") int provinceId
    );

    @GET("api/regions/districts")
    Call<ApiResponse<List<RegionItem>>> getDistricts(
            @Query("cityId") int cityId
    );
}
