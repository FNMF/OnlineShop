package com.example.merchantapp.api.product;

import com.example.merchantapp.model.product.ProductRead;
import com.example.merchantapp.model.product.ProductWriteOptions;

import java.util.List;

import okhttp3.MultipartBody;
import okhttp3.RequestBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.Multipart;
import retrofit2.http.PATCH;
import retrofit2.http.POST;
import retrofit2.http.Part;
import retrofit2.http.Path;

public interface ProductApiService {
    @GET("api/merchant/products")
    Call<List<ProductRead>> getProductReads();
    @GET("api/merchant/products/{uuid}")
    Call<ProductRead> getProductByUuid(@Path("uuid") String uuid);
    @Multipart
    @POST("api/merchant/products")
    Call<ProductRead> addProduct(@Part("ProductName") RequestBody productName,
                                 @Part("ProductPrice") RequestBody productPrice,
                                 @Part("ProductPackingFee") RequestBody productPackingFee,
                                 @Part("ProductStock") RequestBody productStock,
                                 @Part("ProductDescription") RequestBody productDescription,
                                 @Part("ProductIngredient") RequestBody productIngredient,
                                 @Part("ProductWeight") RequestBody productWeight,
                                 @Part("ProductIslisted") RequestBody productIslisted,
                                 @Part("ProductIsavailable") RequestBody productIsavailable,
                                 @Part MultipartBody.Part productCoverFile,
                                 @Part List<MultipartBody.Part> productImages);
    @Multipart
    @PATCH("api/merchant/products/{uuid}")
    Call<ProductRead> updateProduct(@Path("uuid") String uuid,
                                    @Part("ProductName") RequestBody productName,
                                    @Part("ProductPrice") RequestBody productPrice,
                                    @Part("ProductPackingFee") RequestBody productPackingFee,
                                    @Part("ProductStock") RequestBody productStock,
                                    @Part("ProductDescription") RequestBody productDescription,
                                    @Part("ProductIngredient") RequestBody productIngredient,
                                    @Part("ProductWeight") RequestBody productWeight,
                                    @Part("ProductIslisted") RequestBody productIslisted,
                                    @Part("ProductIsavailable") RequestBody productIsavailable,
                                    @Part MultipartBody.Part productCoverFile,
                                    @Part List<MultipartBody.Part> productImages);
    @DELETE("api/merchant/products/{uuid}")
    Call<Void> deleteProduct(@Path("uuid") String uuid);
}
