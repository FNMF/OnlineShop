package com.example.merchantapp.service;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;

import com.example.merchantapp.ui.update.UpdateDialogActivity;

import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class UpdateChecker {

    private static final String PREFS_NAME = "app_update_prefs";
    private static final String KEY_LATEST_VERSION_CODE = "latest_version_code";
    private static final String KEY_LATEST_APK_URL = "latest_apk_url";
    private static final String KEY_LATEST_CHANGELOG = "latest_changelog";

    public static void checkForUpdate(Context context, int currentVersionCode) {
        new Thread(() -> {
            try {
                URL url = new URL("https://example.com/api/app/version");//todo，改成后端api
                HttpURLConnection conn = (HttpURLConnection) url.openConnection();
                conn.setConnectTimeout(5000);
                conn.setReadTimeout(5000);

                BufferedReader reader = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                StringBuilder result = new StringBuilder();
                String line;
                while ((line = reader.readLine()) != null) {
                    result.append(line);
                }
                reader.close();

                JSONObject json = new JSONObject(result.toString());
                int latestVersionCode = json.getInt("latestVersionCode");
                String apkUrl = json.getString("apkUrl");
                boolean forceUpdate = json.optBoolean("forceUpdate", false);
                String changeLog = json.optString("changeLog", "发现新版本");

                if (latestVersionCode > currentVersionCode) {
                    if (forceUpdate) {
                        // 硬更新 -> 直接弹窗
                        Intent dialogIntent = new Intent(context, UpdateDialogActivity.class);
                        dialogIntent.putExtra("apkUrl", apkUrl);
                        dialogIntent.putExtra("forceUpdate", true);
                        dialogIntent.putExtra("changeLog", changeLog);
                        dialogIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                        context.startActivity(dialogIntent);
                    } else {
                        // 软更新 -> 仅保存数据，等待用户主动点击“检查更新”
                        saveUpdateInfo(context, latestVersionCode, apkUrl, changeLog);
                    }
                }
            } catch (Exception e) {
                e.printStackTrace();
            }
        }).start();
    }

    private static void saveUpdateInfo(Context context, int versionCode, String apkUrl, String changeLog) {
        SharedPreferences sp = context.getSharedPreferences(PREFS_NAME, Context.MODE_PRIVATE);
        sp.edit()
                .putInt(KEY_LATEST_VERSION_CODE, versionCode)
                .putString(KEY_LATEST_APK_URL, apkUrl)
                .putString(KEY_LATEST_CHANGELOG, changeLog)
                .apply();
    }

    public static void showUpdateDialogIfAvailable(Context context, int currentVersionCode) {
        SharedPreferences sp = context.getSharedPreferences(PREFS_NAME, Context.MODE_PRIVATE);
        int latestVersionCode = sp.getInt(KEY_LATEST_VERSION_CODE, -1);
        if (latestVersionCode > currentVersionCode) {
            String apkUrl = sp.getString(KEY_LATEST_APK_URL, "");
            String changeLog = sp.getString(KEY_LATEST_CHANGELOG, "发现新版本");

            Intent dialogIntent = new Intent(context, UpdateDialogActivity.class);
            dialogIntent.putExtra("apkUrl", apkUrl);
            dialogIntent.putExtra("forceUpdate", false);
            dialogIntent.putExtra("changeLog", changeLog);
            dialogIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
            context.startActivity(dialogIntent);
        }
    }
}
