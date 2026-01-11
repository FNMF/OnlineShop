package com.example.merchantapp.ui.shopsettings;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

import com.example.merchantapp.model.region.RegionItem;

public class MerchantFormViewModel extends ViewModel {

    private final MutableLiveData<Integer> currentStep =
            new MutableLiveData<>(0);

    private final MutableLiveData<MerchantFormState> formState =
            new MutableLiveData<>(new MerchantFormState());

    public LiveData<Integer> getCurrentStep() {
        return currentStep;
    }

    public MerchantFormState getState() {
        return formState.getValue();
    }

    public void nextStep() {
        Integer step = currentStep.getValue();
        if (step != null && step < 3) {
            currentStep.setValue(step + 1);
        }
    }

    public void prevStep() {
        Integer step = currentStep.getValue();
        if (step != null && step > 0) {
            currentStep.setValue(step - 1);
        }
    }

    /* ===== region ===== */

    public void setProvince(RegionItem p) {
        MerchantFormState s = getState();
        s.provinceId = p.getId();
        s.provinceName = p.getName();
        s.cityId = null;
        s.districtId = null;
    }

    public void setCity(RegionItem c) {
        MerchantFormState s = getState();
        s.cityId = c.getId();
        s.cityName = c.getName();
        s.districtId = null;
    }

    public void setDistrict(RegionItem d) {
        MerchantFormState s = getState();
        s.districtId = d.getId();
        s.districtName = d.getName();
    }

    public void setDetail(String info){
        MerchantFormState s = getState();
        s.detailAddress = info;
    }
}

