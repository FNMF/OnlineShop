package com.example.merchantapp.ui.dashboard;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;

import com.example.merchantapp.databinding.FragmentDashboardBinding;
import com.example.merchantapp.service.ShopAdminGuard;
import com.example.merchantapp.ui.home.HomeViewModel;

public class DashboardFragment extends Fragment {

    private FragmentDashboardBinding binding;

    private ShopAdminGuard shopAdminGuard;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        DashboardViewModel dashboardViewModel =
                new ViewModelProvider(this).get(DashboardViewModel.class);

        binding = FragmentDashboardBinding.inflate(inflater, container, false);
        View root = binding.getRoot();

        shopAdminGuard = new ShopAdminGuard(requireContext());

        if (shopAdminGuard.isShopAdmin()) {
            showContent(dashboardViewModel);
        } else {
            showNoPermission();
        }

        return root;
    }
    private void showContent(DashboardViewModel dashboardViewModel) {
        binding.contentContainer.setVisibility(View.VISIBLE);
        binding.noPermissionContainer.setVisibility(View.GONE);

        dashboardViewModel.getText()
                .observe(getViewLifecycleOwner(), binding.textDashboard::setText);
    }

    private void showNoPermission() {
        binding.contentContainer.setVisibility(View.GONE);
        binding.noPermissionContainer.setVisibility(View.VISIBLE);

        binding.btnApplyShopAdmin.setOnClickListener(v ->
                shopAdminGuard.applyShopAdmin(this::refreshUi)
        );
    }

    private void refreshUi() {
        // 最简单、最稳定的刷新方式
        requireActivity()
                .getSupportFragmentManager()
                .beginTransaction()
                .detach(this)
                .attach(this)
                .commit();
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }
}