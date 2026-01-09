package com.example.merchantapp.ui.auth;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.ViewModelProvider;

import com.example.merchantapp.databinding.ActivityLoginBinding;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.storage.TokenManager;
import com.example.merchantapp.ui.PostLoginLoadingActivity;
import com.example.merchantapp.ui.SplashActivity;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class LoginActivity extends AppCompatActivity {

    private ActivityLoginBinding binding;
    private LoginViewModel loginViewModel;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        binding = ActivityLoginBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());

        // ViewModel
        loginViewModel = new ViewModelProvider(this).get(LoginViewModel.class);

        // 账号密码登录
        binding.login.setOnClickListener(v -> doPasswordLogin());

        // 手机号登录
        binding.phoneLoginButton.setOnClickListener(v -> {
            startActivity(new Intent(this, PhoneActivity.class));
        });
    }

    private void doPasswordLogin() {
        String username = binding.username.getText().toString().trim();
        String password = binding.password.getText().toString().trim();

        if (username.isEmpty() || password.isEmpty()) {
            toast("请输入账号和密码");
            return;
        }


        loginViewModel.loginByAccount(username,password, new Callback<ApiResponse<AuthResponse>>() {
            @Override
            public void onResponse(Call<ApiResponse<AuthResponse>> call,
                                   Response<ApiResponse<AuthResponse>> response) {

                if (response.isSuccessful() && response.body() != null) {
                    ApiResponse<AuthResponse> wrapper = response.body();
                    AuthResponse body = wrapper.getData();

                    // 保存 token & 商户信息
                    TokenManager.saveLogin(
                            LoginActivity.this,
                            body.getLoginResponse().getAccessToken(),
                            body.getLoginResponse().getRefreshToken(),
                            body.getLoginResponse().getMerchant()
                    );

                    goPostLoginLoading();
                } else {
                    toast("账号或密码错误");
                }
            }

            @Override
            public void onFailure(Call<ApiResponse<AuthResponse>> call, Throwable t) {
                toast("网络错误，请稍后重试");
            }
        });
    }

    private void goPostLoginLoading() {
        // 清栈不让返回登录页
        Intent intent = new Intent(LoginActivity.this, PostLoginLoadingActivity.class);
        intent.setFlags(
                Intent.FLAG_ACTIVITY_NEW_TASK |
                        Intent.FLAG_ACTIVITY_CLEAR_TASK
        );
        startActivity(intent);
        finish();
    }

    private void toast(String msg) {
        Toast.makeText(this, msg, Toast.LENGTH_SHORT).show();
    }

    @Override
    public void onBackPressed() {
        Intent intent = new Intent(this, SplashActivity.class);
        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP | Intent.FLAG_ACTIVITY_NEW_TASK);
        startActivity(intent);
        finish();
    }
}
