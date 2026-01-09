package com.example.merchantapp.ui.products;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;
import androidx.navigation.NavController;
import androidx.navigation.fragment.NavHostFragment;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import com.example.merchantapp.R;
import com.example.merchantapp.model.product.ProductRead;
import com.example.merchantapp.service.ShopAdminGuard;
import com.example.merchantapp.viewmodel.ProductViewModel;

import java.util.ArrayList;
import java.util.List; // 引入你实际使用的数据类型

/**
 * A fragment representing a list of Items.
 */
public class ProductFragment extends Fragment {

    private View contentContainer;
    private View noPermissionContainer;
    private RecyclerView recyclerView;

    private ProductViewModel viewModel;
    private ProductAdapter adapter;

    @Override
    public View onCreateView(
            LayoutInflater inflater,
            ViewGroup container,
            Bundle savedInstanceState
    ) {
        View root = inflater.inflate(R.layout.activity_product, container, false);

        contentContainer = root.findViewById(R.id.content_container);
        noPermissionContainer = root.findViewById(R.id.no_permission_container);
        recyclerView = root.findViewById(R.id.recyclerView);
        Button applyBtn = root.findViewById(R.id.btn_apply_shop_admin);
        Button addBtn = root.findViewById(R.id.add_product_button);

        recyclerView.setLayoutManager(new LinearLayoutManager(requireContext()));

        ShopAdminGuard guard = new ShopAdminGuard(requireContext());

        if (!guard.isShopAdmin()) {
            contentContainer.setVisibility(View.GONE);
            noPermissionContainer.setVisibility(View.VISIBLE);

            applyBtn.setOnClickListener(v ->
                    guard.applyShopAdmin(this::refreshPage)
            );
            return root;
        }

        // shop admin
        contentContainer.setVisibility(View.VISIBLE);
        noPermissionContainer.setVisibility(View.GONE);

        viewModel = new ViewModelProvider(this).get(ProductViewModel.class);
        adapter = new ProductAdapter(new ArrayList<>());
        recyclerView.setAdapter(adapter);

        viewModel.getProductListLiveData().observe(
                getViewLifecycleOwner(),
                products -> adapter.setProductList(products)
        );

        viewModel.getAllProducts();

        addBtn.setOnClickListener(v ->
                startActivity(new Intent(requireContext(), AddProductActivity.class))
        );

        return root;
    }

    private void refreshPage() {
        requireActivity().runOnUiThread(() -> {
            // 重新走一遍 Fragment 生命周期
            NavController nav = NavHostFragment.findNavController(this);
            nav.navigate(R.id.navigation_products);
        });
    }
}

