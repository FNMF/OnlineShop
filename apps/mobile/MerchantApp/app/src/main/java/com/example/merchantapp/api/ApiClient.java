package com.example.merchantapp.api;

import com.example.merchantapp.api.product.ProductApiService;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class ApiClient {
    private static final String BASE_URL = "https://api.vesev.top/";//TODO,可以移至配置文件中，这里需要写成后端的api地址
    private static Retrofit retrofit;
    public static Retrofit getClient(){
        if (retrofit == null){
            retrofit = new Retrofit.Builder()
                    .baseUrl(BASE_URL)
                    .addConverterFactory(GsonConverterFactory.create())
                    .build();
        }
        return retrofit;
    }
    //注册Api
    public static ProductApiService getProductService() {
        return getClient().create(ProductApiService.class);
    }
}
