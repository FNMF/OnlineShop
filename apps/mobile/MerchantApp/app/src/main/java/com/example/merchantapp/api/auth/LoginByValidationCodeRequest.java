package com.example.merchantapp.api.auth;

public class LoginByValidationCodeRequest {
    public String phone;
    public String validationCode;
    public LoginByValidationCodeRequest(String phone , String validationCode){
        this.phone = phone;
        this.validationCode = validationCode;
    }
}
