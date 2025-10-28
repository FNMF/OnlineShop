package com.example.merchantapp.ui.products;

import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.example.merchantapp.R;
import com.example.merchantapp.model.product.ProductRead;

import java.util.ArrayList;
import java.util.List;

public class ProductAdapter extends RecyclerView.Adapter<ProductAdapter.ProductViewHolder> {

    private List<ProductRead> productList = new ArrayList<>();

    // 构造（可传空列表）
    public ProductAdapter(List<ProductRead> productList) {
        if (productList != null) this.productList = productList;
    }

    @NonNull
    @Override
    public ProductViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.fragment_item, parent, false);
        return new ProductViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ProductViewHolder holder, int position) {
        ProductRead product = productList.get(position);
        holder.productName.setText(product.getName());
        holder.productPrice.setText("￥" + product.getPrice());
        holder.productStock.setText("库存：" + product.getStock());

        // 加载封面（可能为 null -> Glide 会显示占位图）
        Glide.with(holder.itemView.getContext())
                .load(product.getCoverUrl())
                .placeholder(R.drawable.ic_placeholder)
                .error(R.drawable.ic_image_error)
                .into(holder.productImage);

        // 点击跳转到详情页（假设 product 有 getProductUuid）
        holder.itemView.setOnClickListener(v -> {
            android.content.Intent intent = new android.content.Intent(v.getContext(), com.example.merchantapp.ui.products.ProductDetailActivity.class);
            intent.putExtra("uuid", product.getProductUuid());
            v.getContext().startActivity(intent);
        });
    }

    @Override
    public int getItemCount() {
        return productList.size();
    }

    public void setProductList(List<ProductRead> products) {
        productList.clear();
        if (products != null) productList.addAll(products);
        notifyDataSetChanged();
    }

    public static class ProductViewHolder extends RecyclerView.ViewHolder {
        TextView productName, productPrice, productStock;
        ImageView productImage;

        public ProductViewHolder(View itemView) {
            super(itemView);
            productImage = itemView.findViewById(R.id.product_image);
            productName = itemView.findViewById(R.id.product_name);
            productPrice = itemView.findViewById(R.id.product_price);
            productStock = itemView.findViewById(R.id.product_stock);
        }
    }
}
