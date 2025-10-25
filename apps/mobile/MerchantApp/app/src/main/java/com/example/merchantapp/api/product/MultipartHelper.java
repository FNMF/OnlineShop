package com.example.merchantapp.api.product;

import com.example.merchantapp.model.product.ProductRead;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import okhttp3.MediaType;
import okhttp3.MultipartBody;
import okhttp3.RequestBody;
import retrofit2.Call;

public class MultipartHelper {
    private static RequestBody createPart(String value) {
        return RequestBody.create(MediaType.parse("text/plain"), value);
    }

    public static Call<ProductRead> createProductRequest(
            ProductApiService service,
            String name,
            String price,
            String packingFee,
            String stock,
            String desc,
            String ingredient,
            String weight,
            boolean isListed,
            boolean isAvailable,
            File coverFile,
            List<File> imageFiles
    ) {
        RequestBody namePart = createPart(name);
        RequestBody pricePart = createPart(price);
        RequestBody packingPart = createPart(packingFee);
        RequestBody stockPart = createPart(stock);
        RequestBody descPart = createPart(desc);
        RequestBody ingPart = createPart(ingredient);
        RequestBody weightPart = createPart(weight);
        RequestBody listedPart = createPart(String.valueOf(isListed));
        RequestBody availablePart = createPart(String.valueOf(isAvailable));

        MultipartBody.Part cover = null;
        if (coverFile != null) {
            RequestBody fileBody = RequestBody.create(MediaType.parse("image/*"), coverFile);
            cover = MultipartBody.Part.createFormData("ProductCoverFile", coverFile.getName(), fileBody);
        }

        List<MultipartBody.Part> images = new ArrayList<>();
        if (imageFiles != null) {
            int sort = 1;
            for (File img : imageFiles) {
                RequestBody imgBody = RequestBody.create(MediaType.parse("image/*"), img);
                MultipartBody.Part part = MultipartBody.Part.createFormData(
                        "ProductImages[" + sort + "].ProductImage", img.getName(), imgBody);
                MultipartBody.Part sortPart = MultipartBody.Part.createFormData(
                        "ProductImages[" + sort + "].SortNumber", String.valueOf(sort));
                images.add(part);
                images.add(sortPart);
                sort++;
            }
        }

        return service.addProduct(
                namePart, pricePart, packingPart, stockPart,
                descPart, ingPart, weightPart,
                listedPart, availablePart,
                cover, images
        );
    }

    public static Call<ProductRead> createUpdateProductRequest(
            ProductApiService service,
            String uuid,
            String name,
            String price,
            String packingFee,
            String stock,
            String desc,
            String ingredient,
            String weight,
            boolean isListed,
            boolean isAvailable,
            File coverFile,
            List<File> imageFiles
    ) {
        RequestBody namePart = createPart(name);
        RequestBody pricePart = createPart(price);
        RequestBody packingPart = createPart(packingFee);
        RequestBody stockPart = createPart(stock);
        RequestBody descPart = createPart(desc);
        RequestBody ingPart = createPart(ingredient);
        RequestBody weightPart = createPart(weight);
        RequestBody listedPart = createPart(String.valueOf(isListed));
        RequestBody availablePart = createPart(String.valueOf(isAvailable));

        MultipartBody.Part cover = null;
        if (coverFile != null) {
            RequestBody fileBody = RequestBody.create(MediaType.parse("image/*"), coverFile);
            cover = MultipartBody.Part.createFormData("ProductCoverFile", coverFile.getName(), fileBody);
        }

        List<MultipartBody.Part> images = new ArrayList<>();
        if (imageFiles != null) {
            int sort = 1;
            for (File img : imageFiles) {
                RequestBody imgBody = RequestBody.create(MediaType.parse("image/*"), img);
                MultipartBody.Part part = MultipartBody.Part.createFormData(
                        "ProductImages[" + sort + "].ProductImage", img.getName(), imgBody);
                MultipartBody.Part sortPart = MultipartBody.Part.createFormData(
                        "ProductImages[" + sort + "].SortNumber", String.valueOf(sort));
                images.add(part);
                images.add(sortPart);
                sort++;
            }
        }

        return service.updateProduct(
                uuid,
                namePart, pricePart, packingPart, stockPart,
                descPart, ingPart, weightPart,
                listedPart, availablePart,
                cover, images
        );
    }
}
