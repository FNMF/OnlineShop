package com.example.merchantapp.api;

import android.content.Context;

import com.example.merchantapp.api.auth.AuthRepository;
import com.example.merchantapp.api.merchant.MerchantRepository;
import com.example.merchantapp.api.role.RoleRepository;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.model.merchant.AdminMerchantResponse;
import com.example.merchantapp.storage.AdminManager;
import com.example.merchantapp.storage.RoleManager;
import com.example.merchantapp.storage.ShopManager;

import java.util.List;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.concurrent.atomic.AtomicReference;

import retrofit2.Call;
import retrofit2.Response;

public class UserSessionRepository {

    private final AuthRepository authRepo = new AuthRepository();
    private final MerchantRepository merchantRepo = new MerchantRepository();
    private final RoleRepository roleRepo = new RoleRepository();

    public interface Callback {
        void onSuccess();
        void onError();
    }

    public void fetchSession(Context context, Callback callback) {

        AtomicReference<AuthResponse> adminRef = new AtomicReference<>();
        AtomicReference<AdminMerchantResponse> merchantRef = new AtomicReference<>();
        AtomicReference<List<String>> rolesRef = new AtomicReference<>();

        AtomicInteger successCount = new AtomicInteger(0);
        AtomicBoolean hasError = new AtomicBoolean(false);


        Runnable tryFinish = () -> {
            if (successCount.incrementAndGet() == 3 && !hasError.get()) {
                AdminManager.saveAdmin(adminRef.get().getLoginResponse().getMerchant());
                RoleManager.saveRoles(rolesRef.get());

                // 商户存在才保存
                if (merchantRef.get() != null) {
                    ShopManager.saveShop(merchantRef.get());
                }

                callback.onSuccess();
            }
        };


        authRepo.getMerchantProfile(new retrofit2.Callback<>() {
            @Override
            public void onResponse(Call<ApiResponse<AuthResponse>> call,
                                   Response<ApiResponse<AuthResponse>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    adminRef.set(response.body().getData());
                    tryFinish.run();
                } else {
                    hasError.set(true);
                    callback.onError();
                }
            }

            @Override
            public void onFailure(Call<ApiResponse<AuthResponse>> call, Throwable t) {
                hasError.set(true);
                callback.onError();
            }
        });

        merchantRepo.getMerchant(new retrofit2.Callback<>() {
            @Override
            public void onResponse(Call<ApiResponse<AdminMerchantResponse>> call,
                                   Response<ApiResponse<AdminMerchantResponse>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    merchantRef.set(response.body().getData());
                    tryFinish.run();
                }
            }

            @Override
            public void onFailure(Call<ApiResponse<AdminMerchantResponse>> call, Throwable t) {
                hasError.set(true);
                callback.onError();
            }
        });

        roleRepo.getUserRoles(new retrofit2.Callback<>() {
            @Override
            public void onResponse(Call<ApiResponse<List<String>>> call,
                                   Response<ApiResponse<List<String>>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    rolesRef.set(response.body().getData());
                    tryFinish.run();
                } else {
                    hasError.set(true);
                    callback.onError();
                }
            }

            @Override
            public void onFailure(Call<ApiResponse<List<String>>> call, Throwable t) {
                hasError.set(true);
                callback.onError();
            }
        });
    }
}

