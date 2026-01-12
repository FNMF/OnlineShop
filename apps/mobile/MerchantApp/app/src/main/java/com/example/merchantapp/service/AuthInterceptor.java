package com.example.merchantapp.service;

import android.content.Context;

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
        String access = TokenManager.getAccessToken();

        Request request = chain.request();
        if (access != null) {
            request = request.newBuilder()
                    .header("Authorization", "Bearer " + access)
                    .build();
        }
        return chain.proceed(request);
    }
}


