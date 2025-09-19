package com.example.merchantapp.ui.update;

import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.provider.Settings;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import com.example.merchantapp.R;
import com.example.merchantapp.service.DownloadService;

public class UpdateDialogActivity extends AppCompatActivity {

    private String apkUrl;
    private boolean forceUpdate;
    private String changeLog;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_update_dialog);

        apkUrl = getIntent().getStringExtra("apkUrl");
        forceUpdate = getIntent().getBooleanExtra("forceUpdate", false);
        changeLog = getIntent().getStringExtra("changeLog");

        TextView tvChangeLog = findViewById(R.id.tvChangeLog);
        tvChangeLog.setText(changeLog);

        Button btnUpdate = findViewById(R.id.btnUpdate);
        Button btnCancel = findViewById(R.id.btnCancel);

        btnUpdate.setOnClickListener(v -> startDownload());

        if (forceUpdate) {
            btnCancel.setVisibility(View.GONE);
            setCancelable(false);
        } else {
            btnCancel.setVisibility(View.VISIBLE);
            btnCancel.setOnClickListener(v -> finish());
        }
    }

    private void startDownload() {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            if (!getPackageManager().canRequestPackageInstalls()) {
                // 没有安装未知来源权限
                new AlertDialog.Builder(this)
                        .setTitle("权限请求")
                        .setMessage("需要允许安装未知来源应用才能更新，是否前往设置？")
                        .setPositiveButton("去设置", (dialog, which) -> {
                            Intent intent = new Intent(Settings.ACTION_MANAGE_UNKNOWN_APP_SOURCES,
                                    Uri.parse("package:" + getPackageName()));
                            startActivity(intent);
                        })
                        .setNegativeButton("取消", (dialog, which) -> {
                            if (forceUpdate) {
                                finishAffinity(); // 退出整个app
                            } else {
                                finish();
                            }
                        })
                        .show();
                return;
            }
        }

        Intent intent = new Intent(this, DownloadService.class);
        intent.putExtra("url", apkUrl);
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            startForegroundService(intent);
        } else {
            startService(intent);
        }

        finish();
    }

    private void setCancelable(boolean cancelable) {
        // 禁止返回键关闭
        if (!cancelable) {
            setFinishOnTouchOutside(false);
        }
    }

    @Override
    public void onBackPressed() {
        if (forceUpdate) {
            // 硬更新时返回直接退出
            finishAffinity();
        } else {
            super.onBackPressed();
        }
    }
}
