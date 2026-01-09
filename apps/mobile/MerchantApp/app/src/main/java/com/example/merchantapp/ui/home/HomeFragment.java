package com.example.merchantapp.ui.home;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;

import com.example.merchantapp.databinding.FragmentHomeBinding;
import com.example.merchantapp.service.ShopAdminGuard;

public class HomeFragment extends Fragment {

    private FragmentHomeBinding binding;
    private ShopAdminGuard shopAdminGuard;

    @Override
    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {

        HomeViewModel homeViewModel =
                new ViewModelProvider(this).get(HomeViewModel.class);

        binding = FragmentHomeBinding.inflate(inflater, container, false);
        View root = binding.getRoot();

        shopAdminGuard = new ShopAdminGuard(requireContext());

        if (shopAdminGuard.isShopAdmin()) {
            showContent(homeViewModel);
        } else {
            showNoPermission();
        }

        return root;
    }

    private void showContent(HomeViewModel homeViewModel) {
        binding.contentContainer.setVisibility(View.VISIBLE);
        binding.noPermissionContainer.setVisibility(View.GONE);

        homeViewModel.getText()
                .observe(getViewLifecycleOwner(), binding.textHome::setText);
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
