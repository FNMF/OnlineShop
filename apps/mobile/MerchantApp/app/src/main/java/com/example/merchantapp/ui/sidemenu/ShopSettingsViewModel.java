package com.example.merchantapp.ui.sidemenu;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

import com.example.merchantapp.api.merchant.MerchantRepository;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.merchant.AdminMerchantResponse;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ShopSettingsViewModel extends ViewModel {

    private final MerchantRepository repository = new MerchantRepository();
    private final MutableLiveData<AdminMerchantResponse> merchantLiveData
            = new MutableLiveData<>();

    public LiveData<AdminMerchantResponse> getMerchant() {
        return merchantLiveData;
    }

    public void loadMerchant() {
        repository.getMerchant(new Callback<ApiResponse<AdminMerchantResponse>>() {
            @Override
            public void onResponse(
                    Call<ApiResponse<AdminMerchantResponse>> call,
                    Response<ApiResponse<AdminMerchantResponse>> response
            ) {
                if (response.isSuccessful() && response.body() != null) {
                    merchantLiveData.postValue(response.body().getData());
                } else {
                    merchantLiveData.postValue(null);
                }
            }

            @Override
            public void onFailure(
                    Call<ApiResponse<AdminMerchantResponse>> call,
                    Throwable t
            ) {
                merchantLiveData.postValue(null);
            }
        });
    }
}

