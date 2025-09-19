package com.example.merchantapp.service;

import android.app.Activity;

public class ActivityTracker {
    private static Activity currentActivity;

    public static void setCurrentActivity(Activity activity) {
        currentActivity = activity;
    }

    public static Activity getCurrentActivity() {
        return currentActivity;
    }
}
