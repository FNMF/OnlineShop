package com.example.merchantapp.api.role;

import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.role.RoleResponse;

import retrofit2.Call;
import retrofit2.http.GET;

public interface RoleApiService {
    @GET("api/role")
    Call<ApiResponse<RoleResponse>> getUserRoles();
    @GET("api/role/test")
    Call<ApiResponse> applyTestShopAdminRole();
}
