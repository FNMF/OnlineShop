package com.example.merchantapp.api.role;

import com.example.merchantapp.model.ApiResponse;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;

public interface RoleApiService {
    @GET("api/role")
    Call<ApiResponse<List<String>>> getUserRoles();
    @GET("api/role/test")
    Call<ApiResponse> applyTestShopAdminRole();
}
