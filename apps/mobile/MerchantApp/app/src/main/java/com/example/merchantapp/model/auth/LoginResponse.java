package com.example.merchantapp.model.auth;

import com.google.gson.annotations.SerializedName;

public class LoginResponse {
    @SerializedName("jwt")
    private String accessToken;
    private String refreshToken;
    @SerializedName("readDto")
    private AdminInfo merchant;

    public String getAccessToken() {
        return accessToken;
    }

    public String getRefreshToken() {
        return refreshToken;
    }

    public AdminInfo getMerchant() {
        return merchant;
    }
}
