package com.example.merchantapp.ui.auth;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.ViewModelProvider;

import com.example.merchantapp.R;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.storage.TokenManager;
import com.example.merchantapp.ui.MainActivity;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class RegisterPasswordActivity extends AppCompatActivity {

    private RegisterPasswordViewModel viewModel;
    private EditText passwordEdit;
    private EditText passwordConfirmEdit;
    private Button submitButton;

    private String tempToken;
    private int expiredIn;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register_password);

        viewModel = new ViewModelProvider(this)
                .get(RegisterPasswordViewModel.class);
        passwordEdit = findViewById(R.id.password);
        passwordConfirmEdit = findViewById(R.id.passwordConfirm);
        submitButton = findViewById(R.id.submit);

        tempToken = getIntent().getStringExtra("tempToken");
        expiredIn = getIntent().getIntExtra("expiredIn", 0);

        submitButton.setOnClickListener(v -> submitPassword());
    }

    private void submitPassword() {
        String pwd1 = passwordEdit.getText().toString().trim();
        String pwd2 = passwordConfirmEdit.getText().toString().trim();

        if (pwd1.isEmpty() || pwd2.isEmpty()) {
            toast("请输入两次密码");
            return;
        }

        if (!pwd1.equals(pwd2)) {
            toast("两次密码不一致");
            return;
        }

        viewModel.registerPassword(pwd1, new Callback<ApiResponse<AuthResponse>>() {
            @Override
            public void onResponse(Call<ApiResponse<AuthResponse>> call,
                                   Response<ApiResponse<AuthResponse>> response) {

                if (response.isSuccessful()) {
                    toast("注册成功");

                    ApiResponse<AuthResponse> wrapper = response.body();
                    AuthResponse body = wrapper.getData();

                    TokenManager.saveLogin(
                            RegisterPasswordActivity.this,
                            body.getLoginResponse().getAccessToken(),
                            body.getLoginResponse().getRefreshToken(),
                            body.getLoginResponse().getMerchant()
                    );

                    // 注册完成 → 进 Main，清栈
                    goMainAndClearStack();
                } else {
                    // tempToken 失效 or 业务失败
                    handleRegisterError(response.code());
                }
            }

            @Override
            public void onFailure(Call<ApiResponse<AuthResponse>> call, Throwable t) {
                toast("网络错误，请稍后重试");
            }
        });
    }

    private void toast(String msg) {
        Toast.makeText(this, msg, Toast.LENGTH_SHORT).show();
    }

    private void handleRegisterError(int code) {
        if (code == 401 || code == 403) {
            toast("验证码已过期，请重新验证");

            // 回到验证码登录页，清栈
            Intent intent = new Intent(this, PhoneActivity.class);
            intent.setFlags(
                    Intent.FLAG_ACTIVITY_NEW_TASK |
                            Intent.FLAG_ACTIVITY_CLEAR_TASK
            );
            startActivity(intent);
            finish();
        } else {
            toast("注册失败，请稍后重试");
        }
    }

    private void goMainAndClearStack() {
        Intent intent = new Intent(this, MainActivity.class);
        intent.setFlags(
                Intent.FLAG_ACTIVITY_NEW_TASK |
                        Intent.FLAG_ACTIVITY_CLEAR_TASK
        );
        startActivity(intent);
        finish();
    }

}

