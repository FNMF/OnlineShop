package com.example.merchantapp.api.role;

import com.example.merchantapp.api.auth.AuthApiService;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.role.RoleResponse;
import com.example.merchantapp.service.ApiClient;

import retrofit2.Callback;

public class RoleRepository {
    private final RoleApiService roleApi;
    public RoleRepository(){
        this.roleApi = ApiClient.getRoleService();
    }
    public void getUserRoles(Callback<ApiResponse<RoleResponse>> callback){
        roleApi.getUserRoles().enqueue(callback);
    }
    public void applyTestShopAdmin(Callback<ApiResponse> callback){
        roleApi.applyTestShopAdminRole().enqueue(callback);
    }
}
