package com.example.merchantapp.service;

import com.example.merchantapp.api.auth.AuthApiService;
import com.example.merchantapp.api.product.ProductApiService;

import okhttp3.OkHttpClient;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class ApiClient {

    private static final String BASE_URL = "https://api.vesev.top/";

    private static Retrofit authRetrofit;      // 带拦截器（业务用）
    private static Retrofit rawRetrofit;       // 不带拦截器（刷新用）

    /* ===== 普通业务 Retrofit（自动加 token + 自动 refresh） ===== */
    public static Retrofit getAuthRetrofit() {
        if (authRetrofit == null) {
            OkHttpClient client = new OkHttpClient.Builder()
                    .addInterceptor(new AuthInterceptor(MyApp.appContext()))
                    .authenticator(new TokenAuthenticator(MyApp.appContext()))
                    .build();

            authRetrofit = new Retrofit.Builder()
                    .baseUrl(BASE_URL)
                    .client(client)
                    .addConverterFactory(GsonConverterFactory.create())
                    .build();
        }
        return authRetrofit;
    }

    /* ===== 裸 Retrofit（只给 refresh 用） ===== */
    public static Retrofit getRawRetrofit() {
        if (rawRetrofit == null) {
            rawRetrofit = new Retrofit.Builder()
                    .baseUrl(BASE_URL)
                    .addConverterFactory(GsonConverterFactory.create())
                    .build();
        }
        return rawRetrofit;
    }

    /* ===== Services ===== */

    public static AuthApiService getAuthService() {
        return getAuthRetrofit().create(AuthApiService.class);
    }

    public static AuthApiService getRawAuthService() {
        return getRawRetrofit().create(AuthApiService.class);
    }

    public static ProductApiService getProductService() {
        return getAuthRetrofit().create(ProductApiService.class);
    }
}


