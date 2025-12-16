package com.example.merchantapp.ui.login;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.example.merchantapp.R;
import com.example.merchantapp.ui.SplashActivity;

public class PhoneActivity extends AppCompatActivity {

    private EditText phoneNumberEditText;
    private EditText verificationCodeEditText;
    private Button sendCodeButton;
    private Button loginWithPhoneButton;
    private Button goToPasswordLoginButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_phone);  // 设置为你刚刚创建的布局文件

        // 初始化控件
        phoneNumberEditText = findViewById(R.id.phone_number);
        verificationCodeEditText = findViewById(R.id.verification_code);
        sendCodeButton = findViewById(R.id.send_verification_code);
        loginWithPhoneButton = findViewById(R.id.phone_login);
        goToPasswordLoginButton = findViewById(R.id.login_with_account_password);

        // 设置发送验证码按钮的点击事件
        sendCodeButton.setOnClickListener(view -> {
            String phoneNumber = phoneNumberEditText.getText().toString().trim();
            if (phoneNumber.isEmpty()) {
                Toast.makeText(this, "请输入手机号", Toast.LENGTH_SHORT).show();
            } else {
                // 这里可以调用实际的发送验证码功能，暂时先做个Toast
                Toast.makeText(this, "验证码已发送至 " + phoneNumber, Toast.LENGTH_SHORT).show();
            }
        });

        // 设置手机号登录按钮的点击事件
        loginWithPhoneButton.setOnClickListener(view -> {
            String verificationCode = verificationCodeEditText.getText().toString().trim();
            if (verificationCode.isEmpty()) {
                Toast.makeText(this, "请输入验证码", Toast.LENGTH_SHORT).show();
            } else {
                // 在这里处理验证码登录的逻辑
                Toast.makeText(this, "验证码登录成功", Toast.LENGTH_SHORT).show();
                // 登录成功后可以跳转到主界面
                // startActivity(new Intent(this, MainActivity.class));
                // finish();  // 结束当前活动
            }
        });

        // 设置跳转到密码登录界面的按钮
        goToPasswordLoginButton.setOnClickListener(view -> {
            Intent intent = new Intent(PhoneActivity.this, LoginActivity.class);
            startActivity(intent);
        });
    }
    @Override
    public void onBackPressed() {
        super.onBackPressed();
        // 使用 Intent 跳转回 SplashActivity
        Intent splashIntent = new Intent(PhoneActivity.this, SplashActivity.class);
        splashIntent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP | Intent.FLAG_ACTIVITY_NEW_TASK);  // 清除当前任务栈，创建新的任务栈
        startActivity(splashIntent);
        finish();  // 结束 LoginActivity
    }

}
