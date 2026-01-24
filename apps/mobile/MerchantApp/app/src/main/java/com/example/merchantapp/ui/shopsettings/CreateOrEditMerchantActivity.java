package com.example.merchantapp.ui.shopsettings;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.AutoCompleteTextView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.ViewModelProvider;

import com.example.merchantapp.R;
import com.example.merchantapp.api.region.RegionRepository;
import com.example.merchantapp.model.ApiResponse;
import com.example.merchantapp.model.merchant.AdminMerchantResponse;
import com.example.merchantapp.model.region.RegionItem;
import com.example.merchantapp.storage.ShopManager;
import com.google.android.material.timepicker.MaterialTimePicker;
import com.google.android.material.timepicker.TimeFormat;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CreateOrEditMerchantActivity extends AppCompatActivity {

    /* ---------------- core ---------------- */
    private CreateOrEditMerchantViewModel submitViewModel;

    private MerchantFormViewModel viewModel;
    private RegionRepository regionRepo;
    private AdminMerchantResponse localShop;


    /* ---------------- step containers ---------------- */

    private View stepName, stepRegion, stepTime, stepFee;
    private Button btnPrev, btnNext;

    /* ---------------- Step 1 (name) ---------------- */

    private EditText etShopName;

    /* ---------------- Step 2 (Region) ---------------- */

    private AutoCompleteTextView etProvince, etCity, etDistrict;
    private EditText etDetailAddress;

    private ArrayAdapter<String> provinceAdapter;
    private ArrayAdapter<String> cityAdapter;
    private ArrayAdapter<String> districtAdapter;

    private final List<RegionItem> provinces = new ArrayList<>();
    private final List<RegionItem> cities = new ArrayList<>();
    private final List<RegionItem> districts = new ArrayList<>();

    private final Map<Integer, List<RegionItem>> cityCache = new HashMap<>();
    private final Map<Integer, List<RegionItem>> districtCache = new HashMap<>();

    /* ---------------- Step 3 (time) ---------------- */

    private TextView tvStartTime, tvEndTime;

    /* ---------------- Step 4 (fee) ---------------- */

    private EditText etDeliveryFee, etMinimumAmount, etFreeDelivery;

    /* ================================================= */

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_merchant);

        submitViewModel = new ViewModelProvider(this)
                .get(CreateOrEditMerchantViewModel.class);
        submitViewModel.getSubmitState().observe(this, state -> {
            if (state == null) return;

            switch (state.status) {
                case LOADING:
                    btnNext.setEnabled(false);
                    btnNext.setText("提交中...");
                    break;

                case SUCCESS:
                    Toast.makeText(this, "商户创建成功", Toast.LENGTH_SHORT).show();
                    setResult(RESULT_OK);
                    finish();
                    break;

                case ERROR:
                    btnNext.setEnabled(true);
                    btnNext.setText("完成");
                    Toast.makeText(this, state.errorMessage, Toast.LENGTH_SHORT).show();
                    break;
            }
        });


        viewModel = new ViewModelProvider(this).get(MerchantFormViewModel.class);
        regionRepo = new RegionRepository();

        initViews();
        observeStep();
        setupButtons();

        localShop = ShopManager.getShop();

        if (localShop != null) {
            viewModel.fillFromMerchant(localShop);
            fillUIFromState();
        }
    }

    /* ================================================= */
    /* ================= 初始化 ======================== */
    /* ================================================= */

    private void fillUIFromState() {
        MerchantFormState s = viewModel.getState();

        etShopName.setText(s.name);
        etDetailAddress.setText(s.detailAddress);

        tvStartTime.setText(s.businessStart);
        tvEndTime.setText(s.businessEnd);

        etDeliveryFee.setText(s.deliveryFee.toPlainString());
        etMinimumAmount.setText(s.minimumOrderAmount.toPlainString());
        etFreeDelivery.setText(s.freeDeliveryThreshold.toPlainString());
    }

    private void initViews() {

        stepName = findViewById(R.id.step_name);
        stepRegion = findViewById(R.id.step_region);
        stepTime = findViewById(R.id.step_time);
        stepFee = findViewById(R.id.step_fee);

        btnPrev = findViewById(R.id.btn_prev);
        btnNext = findViewById(R.id.btn_next);

        /* Step 1 */
        etShopName = findViewById(R.id.et_shop_name);

        /* Step 2 */
        etProvince = findViewById(R.id.et_province);
        etCity = findViewById(R.id.et_city);
        etDistrict = findViewById(R.id.et_district);
        etDetailAddress = findViewById(R.id.et_detail);

        initRegionViews();

        /* Step 3 */
        tvStartTime = findViewById(R.id.tv_start_time);
        tvEndTime = findViewById(R.id.tv_end_time);
        setupTimePicker();

        /* Step 4 */
        etDeliveryFee = findViewById(R.id.et_delivery_fee);
        etMinimumAmount = findViewById(R.id.et_minimum_amount);
        etFreeDelivery = findViewById(R.id.et_free_delivery);
    }

    private void initRegionViews() {

        provinceAdapter = new ArrayAdapter<>(this,
                android.R.layout.simple_list_item_1);
        cityAdapter = new ArrayAdapter<>(this,
                android.R.layout.simple_list_item_1);
        districtAdapter = new ArrayAdapter<>(this,
                android.R.layout.simple_list_item_1);

        etProvince.setAdapter(provinceAdapter);
        etCity.setAdapter(cityAdapter);
        etDistrict.setAdapter(districtAdapter);

        etProvince.setOnClickListener(v -> etProvince.showDropDown());
        etCity.setOnClickListener(v -> etCity.showDropDown());
        etDistrict.setOnClickListener(v -> etDistrict.showDropDown());

        etCity.setEnabled(false);
        etDistrict.setEnabled(false);

        bindRegionEvents();
        loadProvinces();
    }

    /* ================================================= */
    /* ================= Step 控制 ===================== */
    /* ================================================= */

    private void observeStep() {
        viewModel.getCurrentStep().observe(this, step -> {

            hideAllSteps();

            btnPrev.setVisibility(step == 0 ? View.INVISIBLE : View.VISIBLE);
            btnNext.setText(step == 3 ? "完成" : "下一步");

            if (step == 0) stepName.setVisibility(View.VISIBLE);
            if (step == 1) stepRegion.setVisibility(View.VISIBLE);
            if (step == 2) stepTime.setVisibility(View.VISIBLE);
            if (step == 3) stepFee.setVisibility(View.VISIBLE);
        });
    }

    private void setupButtons() {
        btnPrev.setOnClickListener(v -> viewModel.prevStep());

        btnNext.setOnClickListener(v -> {
            int step = viewModel.getCurrentStep().getValue();

            if (!validateStep(step)) return;

            saveCurrentStepData();

            if (step == 3) submitForm();
            else viewModel.nextStep();
        });
    }

    private void hideAllSteps() {
        stepName.setVisibility(View.GONE);
        stepRegion.setVisibility(View.GONE);
        stepTime.setVisibility(View.GONE);
        stepFee.setVisibility(View.GONE);
    }

    /* ================================================= */
    /* ================= Region ======================== */
    /* ================================================= */

    private void bindRegionEvents() {

        etProvince.setOnItemClickListener((p, v, pos, id) -> {
            RegionItem province = provinces.get(pos);
            viewModel.setProvince(province);

            etCity.setText("", false);
            etDistrict.setText("", false);

            etCity.setEnabled(true);
            etDistrict.setEnabled(false);

            loadCities(province.getId());
        });

        etCity.setOnItemClickListener((p, v, pos, id) -> {
            RegionItem city = cities.get(pos);
            viewModel.setCity(city);

            etDistrict.setText("", false);
            etDistrict.setEnabled(true);

            loadDistricts(city.getId());
        });

        etDistrict.setOnItemClickListener((p, v, pos, id) ->
                viewModel.setDistrict(districts.get(pos))
        );
    }

    private void loadProvinces() {
        regionRepo.getProvinces(new Callback<ApiResponse<List<RegionItem>>>() {
            @Override
            public void onResponse(Call<ApiResponse<List<RegionItem>>> call,
                                   Response<ApiResponse<List<RegionItem>>> response) {

                provinces.clear();
                provinces.addAll(response.body().getData());

                provinceAdapter.clear();
                for (RegionItem r : provinces) {
                    provinceAdapter.add(r.getName());
                }
            }

            @Override public void onFailure(Call<ApiResponse<List<RegionItem>>> call, Throwable t) {}
        });
    }

    private void loadCities(int provinceId) {

        if (cityCache.containsKey(provinceId)) {
            bindCities(cityCache.get(provinceId));
            return;
        }

        regionRepo.getCities(provinceId, new Callback<ApiResponse<List<RegionItem>>>() {
            @Override
            public void onResponse(Call<ApiResponse<List<RegionItem>>> call,
                                   Response<ApiResponse<List<RegionItem>>> response) {

                cityCache.put(provinceId, response.body().getData());
                bindCities(response.body().getData());
            }

            @Override public void onFailure(Call<ApiResponse<List<RegionItem>>> call, Throwable t) {}
        });
    }

    private void bindCities(List<RegionItem> data) {
        cities.clear();
        cities.addAll(data);

        cityAdapter.clear();
        for (RegionItem r : cities) cityAdapter.add(r.getName());
    }

    private void loadDistricts(int cityId) {

        if (districtCache.containsKey(cityId)) {
            bindDistricts(districtCache.get(cityId));
            return;
        }

        regionRepo.getDistricts(cityId, new Callback<ApiResponse<List<RegionItem>>>() {
            @Override
            public void onResponse(Call<ApiResponse<List<RegionItem>>> call,
                                   Response<ApiResponse<List<RegionItem>>> response) {

                districtCache.put(cityId, response.body().getData());
                bindDistricts(response.body().getData());
            }

            @Override public void onFailure(Call<ApiResponse<List<RegionItem>>> call, Throwable t) {}
        });
    }

    private void bindDistricts(List<RegionItem> data) {
        districts.clear();
        districts.addAll(data);

        districtAdapter.clear();
        for (RegionItem r : districts) districtAdapter.add(r.getName());
    }

    /* ================================================= */
    /* ================= Time ========================== */
    /* ================================================= */

    private void setupTimePicker() {
        tvStartTime.setOnClickListener(v -> pickTime(true));
        tvEndTime.setOnClickListener(v -> pickTime(false));
    }

    private void pickTime(boolean isStart) {

        MaterialTimePicker picker = new MaterialTimePicker.Builder()
                .setTimeFormat(TimeFormat.CLOCK_24H)
                .build();

        picker.show(getSupportFragmentManager(), "time");

        picker.addOnPositiveButtonClickListener(v -> {
            String time = String.format("%02d:%02d",
                    picker.getHour(), picker.getMinute());

            MerchantFormState s = viewModel.getState();

            if (isStart) {
                s.businessStart = time;
                tvStartTime.setText(time);
            } else {
                s.businessEnd = time;
                tvEndTime.setText(time);
            }
        });
    }

    /* ================================================= */
    /* ================= 校验 & 提交 =================== */
    /* ================================================= */

    private boolean validateStep(int step) {
        return step == 0 ? validateName()
                : step == 1 ? validateRegion()
                : step == 2 ? validateTime()
                : validateFee();
    }

    private void saveCurrentStepData() {
        MerchantFormState s = viewModel.getState();

        s.name = etShopName.getText().toString().trim();
        s.detailAddress = etDetailAddress.getText().toString().trim();
    }

    private void submitForm() {
        submitViewModel.submit(viewModel.getState());
    }

    private boolean validateName() {
        if (etShopName.getText().toString().trim().isEmpty()) {
            etShopName.setError("请输入店铺名称");
            return false;
        }
        return true;
    }

    private boolean validateRegion() {
        MerchantFormState s = viewModel.getState();
        if (s.provinceId == null || s.cityId == null || s.districtId == null) {
            toast("请选择完整地址");
            return false;
        }
        return true;
    }

    private boolean validateTime() {
        MerchantFormState s = viewModel.getState();
        if (s.businessStart == null || s.businessEnd == null) {
            toast("请选择营业时间");
            return false;
        }
        if (s.businessStart.compareTo(s.businessEnd) >= 0) {
            toast("结束时间必须晚于开始时间");
            return false;
        }
        return true;
    }

    private boolean validateFee() {
        try {
            MerchantFormState s = viewModel.getState();

            s.deliveryFee = new BigDecimal(etDeliveryFee.getText().toString());
            s.minimumOrderAmount = new BigDecimal(etMinimumAmount.getText().toString());
            s.freeDeliveryThreshold = new BigDecimal(etFreeDelivery.getText().toString());

            return true;
        } catch (Exception e) {
            toast("请输入正确的金额");
            return false;
        }
    }

    private void toast(String msg) {
        Toast.makeText(this, msg, Toast.LENGTH_SHORT).show();
    }
}
