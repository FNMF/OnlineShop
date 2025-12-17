package com.example.merchantapp.api.auth;

public class LoginByValidationCodeRequest {
    public String phone;
    public String code;
    public LoginByValidationCodeRequest(String phone , String code){
        this.phone = phone;
        this.code = code;
    }
}
