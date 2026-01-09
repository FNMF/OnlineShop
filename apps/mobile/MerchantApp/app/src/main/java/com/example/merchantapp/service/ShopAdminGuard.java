package com.example.merchantapp.service;

import android.content.Context;
import android.widget.Toast;

import com.example.merchantapp.api.role.RoleRepository;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.role.RoleResponse;
import com.example.merchantapp.storage.RoleManager;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ShopAdminGuard {

    private final Context context;
    private final RoleRepository roleRepository = new RoleRepository();

    public ShopAdminGuard(Context context) {
        this.context = context.getApplicationContext();
    }

    public boolean isShopAdmin() {
        return RoleManager.isShopOwner(context);
    }

    public void applyShopAdmin(Runnable onSuccess) {
        roleRepository.applyTestShopAdmin(new Callback<ApiResponse>() {
            @Override
            public void onResponse(Call<ApiResponse> call,
                                   Response<ApiResponse> response) {

                if (response.isSuccessful()
                        && response.body() != null
                        && response.body().isSuccess()) {

                    refreshRole(onSuccess);

                } else {
                    toast("申请失败，请稍后重试");
                }
            }

            @Override
            public void onFailure(Call<ApiResponse> call, Throwable t) {
                toast("网络错误");
            }
        });
    }

    private void refreshRole(Runnable onSuccess) {
        roleRepository.getUserRoles(new Callback<ApiResponse<RoleResponse>>() {
            @Override
            public void onResponse(Call<ApiResponse<RoleResponse>> call,
                                   Response<ApiResponse<RoleResponse>> response) {

                if (response.isSuccessful() && response.body() != null) {
                    RoleResponse data = response.body().getData();
                    if (data != null && data.getRoles() != null) {
                        RoleManager.saveRoles(context, data.getRoles());
                    }
                }
                onSuccess.run();
            }

            @Override
            public void onFailure(Call<ApiResponse<RoleResponse>> call, Throwable t) {
                onSuccess.run();
            }
        });
    }

    private void toast(String msg) {
        Toast.makeText(context, msg, Toast.LENGTH_SHORT).show();
    }
}
