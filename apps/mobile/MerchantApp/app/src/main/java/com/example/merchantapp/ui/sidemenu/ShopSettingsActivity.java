package com.example.merchantapp.ui.sidemenu;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import androidx.activity.result.ActivityResultLauncher;
import androidx.activity.result.contract.ActivityResultContracts;
import androidx.appcompat.widget.Toolbar;
import androidx.lifecycle.ViewModelProvider;

import com.example.merchantapp.R;
import com.example.merchantapp.model.merchant.AdminMerchantResponse;
import com.example.merchantapp.service.ShopAdminGuard;
import com.example.merchantapp.storage.ShopManager;
import com.example.merchantapp.ui.BaseActivity;
import com.example.merchantapp.ui.shopsettings.CreateOrEditMerchantActivity;

public class ShopSettingsActivity extends BaseActivity {

    private ShopSettingsViewModel viewModel;

    private View contentContainer;
    private View noPermissionContainer;
    private View noMerchantContainer;


    private TextView tvShopName;
    private TextView tvShopAddress;
    private TextView tvBusinessTime;
    private TextView tvDeliveryInfo;

    private ShopAdminGuard shopAdminGuard;
    private boolean merchantLoaded = false; // 防止重复加载

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_shop_setting);

        shopAdminGuard = new ShopAdminGuard(this);

        setupToolbar();
        initBaseViews();
        initViewModel();
        refreshUi();
    }

    /** ===== Toolbar 只初始化一次 ===== */
    private void setupToolbar() {
        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        toolbar.setNavigationOnClickListener(v -> finish());
    }

    /** ===== 初始化所有 View ===== */
    private void initBaseViews() {
        contentContainer = findViewById(R.id.content_container);
        noPermissionContainer = findViewById(R.id.no_permission_container);
        noMerchantContainer = findViewById(R.id.no_merchant_container);

        tvShopName = findViewById(R.id.tv_shop_name);
        tvShopAddress = findViewById(R.id.tv_shop_address);
        tvBusinessTime = findViewById(R.id.tv_business_time);
        tvDeliveryInfo = findViewById(R.id.tv_delivery_info);

        Button applyBtn = findViewById(R.id.btn_apply_shop_admin);
        applyBtn.setOnClickListener(v ->
                shopAdminGuard.applyShopAdmin(this::refreshUi)
        );
        Button createBtn = findViewById(R.id.btn_create_shop);
        createBtn.setOnClickListener(v -> {
            Intent intent = new Intent(ShopSettingsActivity.this, CreateOrEditMerchantActivity.class);
            createOrEditMerchantLauncher.launch(intent);
        });
        Button editBtn = findViewById(R.id.btn_edit_shop);
        editBtn.setOnClickListener(v -> {
            Intent intent = new Intent(ShopSettingsActivity.this, CreateOrEditMerchantActivity.class);
            createOrEditMerchantLauncher.launch(intent);
        });
    }

    private ActivityResultLauncher<Intent> createOrEditMerchantLauncher =
            registerForActivityResult(
                    new ActivityResultContracts.StartActivityForResult(),
                    result -> {
                        if (result.getResultCode() == RESULT_OK) {
                            refreshUi(); // 创建完成后刷新
                        }
                    }
            );

    /** ===== ViewModel 初始化一次 ===== */
    private void initViewModel() {
        viewModel = new ViewModelProvider(this)
                .get(ShopSettingsViewModel.class);

        viewModel.getMerchant().observe(this, merchant -> {
            if (merchant == null) {
                showNoMerchant();
                return;
            }

            showContent();
            bindMerchantInfo(merchant);
        });
    }

    /** ===== 权限刷新 ===== */
    private void refreshUi() {
        if (shopAdminGuard.isShopAdmin()) {
            loadMerchantIfNeeded();
        } else {
            showNoPermission();
        }
    }

    private void showContent() {
        contentContainer.setVisibility(View.VISIBLE);
        noPermissionContainer.setVisibility(View.GONE);
        noMerchantContainer.setVisibility(View.GONE);
    }

    private void showNoPermission() {
        contentContainer.setVisibility(View.GONE);
        noPermissionContainer.setVisibility(View.VISIBLE);
        noMerchantContainer.setVisibility(View.GONE);
    }

    private void showNoMerchant() {
        contentContainer.setVisibility(View.GONE);
        noPermissionContainer.setVisibility(View.GONE);
        noMerchantContainer.setVisibility(View.VISIBLE);
    }

    /** ===== 数据只加载一次 ===== */
    private void loadMerchantIfNeeded() {
        if (!merchantLoaded) {
            merchantLoaded = true;
            viewModel.loadMerchant();
        }
    }

    /** ===== 数据绑定 ===== */
    private void bindMerchantInfo(AdminMerchantResponse merchant) {
        tvShopName.setText(merchant.getName());

        tvShopAddress.setText(
                merchant.getProvince()
                        + merchant.getCity()
                        + merchant.getDistrict()
                        + " "
                        + merchant.getDetail()
        );

        tvBusinessTime.setText(
                merchant.getBusinessStart()
                        + " - "
                        + merchant.getBusinessEnd()
        );

        tvDeliveryInfo.setText(
                "配送费 ¥" + merchant.getDeliveryFee()
                        + " |起送 ¥" + merchant.getMinimumOrderAmount()
                        + " |免配送费要求 ¥" + merchant.getFreeDeliveryThreshold()
        );
    }
}
