package com.example.merchantapp.api.auth;

public class LoginByAccountRequest {
    public String account;
    public String password;
    public LoginByAccountRequest(String account, String password){
        this.account = account;
        this.password = password;
    }
}
