package com.example.merchantapp.ui;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import com.example.merchantapp.R;
import com.example.merchantapp.api.auth.AuthRepository;
import com.example.merchantapp.api.merchant.MerchantRepository;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.model.auth.LoginResponse;
import com.example.merchantapp.storage.TokenManager;
import com.example.merchantapp.ui.auth.LoginActivity;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SplashActivity extends AppCompatActivity {

    private MerchantRepository merchantRepository;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_splash);
        merchantRepository = new MerchantRepository();

        autoLogin();
        }

    private void autoLogin() {
        if (!TokenManager.hasRefreshToken(this)) {
            goLogin();
            return;
        }

        merchantRepository.getMerchantProfile(
                new Callback<ApiResponse<AuthResponse>>() {
                    @Override
                    public void onResponse(
                            Call<ApiResponse<AuthResponse>> call,
                            Response<ApiResponse<AuthResponse>> response
                    ) {
                        if (!response.isSuccessful()
                                || response.body() == null
                                || response.body().getData() == null) {

                            TokenManager.clearAll(SplashActivity.this);
                            goLogin();
                            return;
                        }

                        AuthResponse body = response.body().getData();
                        LoginResponse login = body.getLoginResponse();

                        if (login == null) {
                            TokenManager.clearAll(SplashActivity.this);
                            goLogin();
                            return;
                        }

                        TokenManager.saveLogin(
                                SplashActivity.this,
                                login.getAccessToken(),
                                login.getRefreshToken(),
                                login.getMerchant()
                        );

                        goMain();
                    }

                    @Override
                    public void onFailure(
                            Call<ApiResponse<AuthResponse>> call,
                            Throwable t
                    ) {
                        goLogin();
                    }
                });
    }

    private void goMain() {
        Intent intent = new Intent(this, MainActivity.class);
        intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        startActivity(intent);
        finish();
    }

    private void goLogin() {
        Intent intent = new Intent(this, LoginActivity.class);
        intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        startActivity(intent);
        finish();
    }
}