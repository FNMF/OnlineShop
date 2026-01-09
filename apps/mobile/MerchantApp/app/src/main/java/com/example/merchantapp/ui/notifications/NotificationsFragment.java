package com.example.merchantapp.ui.notifications;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;

import com.example.merchantapp.databinding.FragmentNotificationsBinding;
import com.example.merchantapp.service.ShopAdminGuard;
import com.example.merchantapp.ui.home.HomeViewModel;

public class NotificationsFragment extends Fragment {

    private FragmentNotificationsBinding binding;

    private ShopAdminGuard shopAdminGuard;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        NotificationsViewModel notificationsViewModel =
                new ViewModelProvider(this).get(NotificationsViewModel.class);

        binding = FragmentNotificationsBinding.inflate(inflater, container, false);
        View root = binding.getRoot();

        shopAdminGuard = new ShopAdminGuard(requireContext());

        if (shopAdminGuard.isShopAdmin()) {
            showContent(notificationsViewModel);
        } else {
            showNoPermission();
        }

        return root;
    }
    private void showContent(NotificationsViewModel notificationsViewModel) {
        binding.contentContainer.setVisibility(View.VISIBLE);
        binding.noPermissionContainer.setVisibility(View.GONE);

        notificationsViewModel.getText()
                .observe(getViewLifecycleOwner(), binding.textNotifications::setText);
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