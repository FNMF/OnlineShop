package com.example.merchantapp.storage;

import android.content.Context;

public class SessionManager {
    private SessionManager() {}

    /** 退出登录 / 清空会话 */
    public static void logout(Context context) {
        TokenManager.clearAll(context);
        RoleManager.clear(context);
        // TODO,以后有就加
        // UserManager.clear(context);
        // PushManager.clear(context);
    }

    /** 是否已登录 */
    public static boolean isLoggedIn(Context context) {
        return TokenManager.getAccessToken(context) != null;
    }
}
