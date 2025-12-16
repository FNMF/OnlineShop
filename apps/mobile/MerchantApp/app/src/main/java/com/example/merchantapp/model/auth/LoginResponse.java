package com.example.merchantapp.model.auth;

public class LoginResponse {
    private String accessToken;
    private String refreshToken;
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
