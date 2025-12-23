package com.example.merchantapp.model.auth;

import com.google.gson.annotations.SerializedName;

public class LoginResponse {
    @SerializedName("jwt")
    private String accessToken;
    private String refreshToken;
    @SerializedName("readDto")
    private MerchantInfo merchant;

    public String getAccessToken() {
        return accessToken;
    }

    public String getRefreshToken() {
        return refreshToken;
    }

    public MerchantInfo getMerchant() {
        return merchant;
    }
}
