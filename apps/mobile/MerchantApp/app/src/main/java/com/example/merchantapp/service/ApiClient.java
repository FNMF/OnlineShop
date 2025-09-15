package com.example.merchantapp.service;

import okhttp3.Interceptor;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

import java.io.IOException;
public class ApiClient {
    private static Retrofit retrofit;

    public static void init(String baseUrl) {
        // 初始化 OkHttp
        OkHttpClient client = new OkHttpClient.Builder()
                .addInterceptor(new Interceptor() {
                    @Override
                    public Response intercept(Chain chain) throws IOException {
                        Request original = chain.request();
                        Request.Builder builder = original.newBuilder();

                        // 自动加上 token
                        String token = UserManager.getInstance().getToken();
                        if (token != null) {
                            builder.header("Authorization", "Bearer " + token);
                        }

                        return chain.proceed(builder.build());
                    }
                })
                .build();

        // 初始化 Retrofit
        retrofit = new Retrofit.Builder()
                .baseUrl(baseUrl)
                .client(client)
                .addConverterFactory(GsonConverterFactory.create()) // Gson 解析
                .build();
    }

    public static Retrofit getRetrofit() {
        if (retrofit == null) {
            throw new IllegalStateException("ApiClient 未初始化，请先调用 ApiClient.init()");
        }
        return retrofit;
    }
}
