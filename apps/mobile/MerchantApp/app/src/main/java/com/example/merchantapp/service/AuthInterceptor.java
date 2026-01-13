package com.example.merchantapp.service;

import android.content.Context;
import android.util.Log;

import com.example.merchantapp.storage.TokenManager;

import java.io.IOException;

import okhttp3.Interceptor;
import okhttp3.Request;
import okhttp3.Response;

public class AuthInterceptor implements Interceptor {

    private final Context context;

    public AuthInterceptor(Context context) {
        this.context = context.getApplicationContext();
    }

    @Override
    public Response intercept(Chain chain) throws IOException {
        Request request = chain.request();
        Log.d("HTTP", "Content-Type = " + request.body().contentType());

        if (request.header("Authorization") == null) {
            String access = TokenManager.getAccessToken();
            if (access != null) {
                request = request.newBuilder()
                        .header("Authorization", "Bearer " + access)
                        .build();
            }
        }
        return chain.proceed(request);
    }
}


