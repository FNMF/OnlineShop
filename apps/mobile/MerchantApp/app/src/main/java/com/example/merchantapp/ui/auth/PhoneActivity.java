package com.example.merchantapp.ui.auth;

import android.content.Intent;
import android.media.session.MediaSession;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.os.TokenWatcher;
import android.util.Log;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.ViewModelProvider;

import com.example.merchantapp.databinding.ActivityPhoneBinding;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.model.auth.LoginResponse;
import com.example.merchantapp.model.auth.RegisterResponse;
import com.example.merchantapp.storage.AdminManager;
import com.example.merchantapp.storage.TokenManager;
import com.example.merchantapp.ui.PostLoginLoadingActivity;
import com.example.merchantapp.ui.SplashActivity;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class PhoneActivity extends AppCompatActivity {

    private ActivityPhoneBinding binding;
    private PhoneViewModel viewModel;

    private CountDownTimer countDownTimer;
    private static final int CODE_COUNTDOWN = 60; // 秒

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        binding = ActivityPhoneBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());

        viewModel = new ViewModelProvider(this).get(PhoneViewModel.class);

        // 发送验证码
        binding.sendVerificationCode.setOnClickListener(v -> sendCode());

        // 验证码登录
        binding.phoneLogin.setOnClickListener(v -> doPhoneLogin());

        // 切回账号密码登录
        binding.loginWithAccountPassword.setOnClickListener(v -> finish());
    }

    private void sendCode() {
        String phone = binding.phoneNumber.getText().toString().trim();

        if (phone.isEmpty() || phone.length() != 11) {
            toast("请输入正确的手机号");
            return;
        }

        // 防止连续点击
        binding.sendVerificationCode.setEnabled(false);

        viewModel.sendCode(phone, new Callback<ApiResponse<Object>>() {
            @Override
            public void onResponse(Call<ApiResponse<Object>> call, Response<ApiResponse<Object>> response) {
                if (response.isSuccessful()) {
                    toast("验证码已发送");
                    startCountDown(); // 成功才开始倒计时
                } else {
                    toast("发送失败，请稍后重试");
                    binding.sendVerificationCode.setEnabled(true);
                }
            }

            @Override
            public void onFailure(Call<ApiResponse<Object>> call, Throwable t) {
                toast("网络错误");
                Log.e("NET", "sendCode failed", t);
                binding.sendVerificationCode.setEnabled(true);
            }
        });
    }

    /** 60 秒倒计时 */
    private void startCountDown() {
        // 防止重复创建
        if (countDownTimer != null) {
            countDownTimer.cancel();
        }

        countDownTimer = new CountDownTimer(CODE_COUNTDOWN * 1000L, 1000L) {
            @Override
            public void onTick(long millisUntilFinished) {
                long second = millisUntilFinished / 1000;
                binding.sendVerificationCode.setText(second + "s 后重试");
                binding.sendVerificationCode.setEnabled(false);
                binding.sendVerificationCode.setAlpha(0.6f); // 变灰
            }

            @Override
            public void onFinish() {
                binding.sendVerificationCode.setText("发送验证码");
                binding.sendVerificationCode.setEnabled(true);
                binding.sendVerificationCode.setAlpha(1f);
            }
        };
        countDownTimer.start();
    }

    private void doPhoneLogin() {
        String phone = binding.phoneNumber.getText().toString().trim();
        String validationCode = binding.verificationCode.getText().toString().trim();

        if (phone.isEmpty() || validationCode.isEmpty()) {
            toast("请输入手机号和验证码");
            return;
        }

        viewModel.loginByValidationCode(phone, validationCode, new Callback<ApiResponse<AuthResponse>>() {
            @Override
            public void onResponse(Call<ApiResponse<AuthResponse>> call,
                                   Response<ApiResponse<AuthResponse>> response) {
                if (!response.isSuccessful() || response.body() == null || response.body().getData() == null) {
                    toast("验证码错误或已过期");
                    return;
                }

                AuthResponse body = response.body().getData();

                if (Boolean.TRUE.equals(body.isNewUser())) {
                    // 新用户 → 跳转设置密码
                    RegisterResponse register = body.getRegisterResponse();
                    if (register != null) {
                        Intent intent = new Intent(PhoneActivity.this, RegisterPasswordActivity.class);
                        intent.putExtra("tempToken", register.getTempToken());
                        intent.putExtra("expiresIn", register.getExpiresIn());
                        startActivity(intent);
                        finish();
                    } else {
                        toast("无法获取新用户信息，请重新获取验证码");
                    }
                } else {
                    // 老用户 → 登录成功后直接跳转
                    goPostLoginLoading();
                }
            }

            @Override
            public void onFailure(Call<ApiResponse<AuthResponse>> call, Throwable t) {
                toast("网络错误，请稍后重试");
            }
        });
    }

    private void goPostLoginLoading() {
        Intent intent = new Intent(this, PostLoginLoadingActivity.class);
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
    protected void onDestroy() {
        super.onDestroy();
        // 防止内存泄漏
        if (countDownTimer != null) {
            countDownTimer.cancel();
        }
    }

    @Override
    public void onBackPressed() {
        Intent intent = new Intent(this, SplashActivity.class);
        intent.setFlags(
                Intent.FLAG_ACTIVITY_CLEAR_TOP |
                        Intent.FLAG_ACTIVITY_NEW_TASK
        );
        startActivity(intent);
        finish();
    }
}
