package com.example.merchantapp.model;

import com.example.merchantapp.model.auth.AuthResponse;
import com.example.merchantapp.model.merchant.AdminMerchantResponse;

import java.util.List;

public class UserSessionSnapshot {
    public AuthResponse admin;
    public AdminMerchantResponse merchant;
    public List<String> roles;
}
