package com.example.merchantapp.service;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class NetworkUtils {

    public interface BackendCheckCallback {
        void onBackendAvailable(String message);
        void onBackendError(String message);
    }

    public static boolean isNetworkAvailable(Context context) {
        ConnectivityManager cm = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo info = cm != null ? cm.getActiveNetworkInfo() : null;
        return info != null && info.isConnected();
    }

    public static void checkBackend(Context context, BackendCheckCallback callback) {
        if (!isNetworkAvailable(context)) {
            // 没有网络，直接提示
            callback.onBackendError("请检查网络后重试");
            return;
        }

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl("https://your-api.com/") // TODO: 改成你的后端地址
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        ApiService api = retrofit.create(ApiService.class);
        Call<HealthResponse> call = api.ping();
        call.enqueue(new Callback<HealthResponse>() {
            @Override
            public void onResponse(Call<HealthResponse> call, Response<HealthResponse> response) {
                if (response.isSuccessful() && response.body() != null) {
                    HealthResponse body = response.body();
                    if (body.success) {
                        callback.onBackendAvailable(body.message);
                    } else {
                        callback.onBackendError(body.message);
                    }
                } else {
                    callback.onBackendError("服务器繁忙，请稍后重试");
                }
            }

            @Override
            public void onFailure(Call<HealthResponse> call, Throwable t) {
                callback.onBackendError("服务器繁忙，请稍后重试");
            }
        });
    }

    // 健康检查接口
    public interface ApiService {
        @retrofit2.http.GET("health") // 健康检查 API
        Call<HealthResponse> ping();
    }

    public static class HealthResponse {
        public boolean success;
        public String message;
    }
}
