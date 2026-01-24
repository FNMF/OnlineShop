package com.example.merchantapp.ui.shopsettings;

import android.util.Log;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

import com.example.merchantapp.api.merchant.MerchantRepository;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.SubmitState;
import com.example.merchantapp.model.merchant.AdminMerchantResponse;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CreateOrEditMerchantViewModel extends ViewModel {

    private final MerchantRepository repository = new MerchantRepository();

    private final MutableLiveData<SubmitState> submitState =
            new MutableLiveData<>(new SubmitState());

    public LiveData<SubmitState> getSubmitState() {
        return submitState;
    }

    public void submit(MerchantFormState form) {

        submitState.setValue(SubmitState.loading());
        if(form.uuid == null) {
            repository.createMerchant(
                    form.name,
                    form.provinceName,
                    form.cityName,
                    form.districtName,
                    form.detailAddress,
                    form.businessStart,
                    form.businessEnd,
                    form.deliveryFee,
                    form.minimumOrderAmount,
                    form.freeDeliveryThreshold,
                    new Callback<ApiResponse<AdminMerchantResponse>>() {

                        @Override
                        public void onResponse(Call<ApiResponse<AdminMerchantResponse>> call,
                                               Response<ApiResponse<AdminMerchantResponse>> response) {
                            if (response.isSuccessful()
                                    && response.body() != null
                                    && response.body().isSuccess()) {

                                submitState.postValue(SubmitState.success());

                            } else {
                                submitState.postValue(
                                        SubmitState.error("创建商户失败")
                                );
                            }
                        }

                        @Override
                        public void onFailure(Call<ApiResponse<AdminMerchantResponse>> call,
                                              Throwable t) {
                            submitState.postValue(
                                    SubmitState.error(t.getMessage())
                            );
                        }
                    }
            );
        }else{
            repository.updateMerchant(
                    form.name,
                    form.provinceName,
                    form.cityName,
                    form.districtName,
                    form.detailAddress,
                    form.businessStart,
                    form.businessEnd,
                    form.deliveryFee,
                    form.minimumOrderAmount,
                    form.freeDeliveryThreshold,
                    new Callback<ApiResponse<AdminMerchantResponse>>() {

                        @Override
                        public void onResponse(Call<ApiResponse<AdminMerchantResponse>> call,
                                               Response<ApiResponse<AdminMerchantResponse>> response) {
                            if (response.isSuccessful()
                                    && response.body() != null
                                    && response.body().isSuccess()) {

                                submitState.postValue(SubmitState.success());

                            } else {
                                submitState.postValue(
                                        SubmitState.error("修改商户失败")
                                );
                            }
                        }

                        @Override
                        public void onFailure(Call<ApiResponse<AdminMerchantResponse>> call,
                                              Throwable t) {
                            submitState.postValue(
                                    SubmitState.error(t.getMessage())
                            );
                        }
                    }
            );
        }
    }
}
