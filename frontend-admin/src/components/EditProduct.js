import React, { useEffect, useState } from "react";
import { makeStyles } from "@mui/styles";
import Paper from "@mui/material/Paper";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import Alert from "@mui/material/Alert";
import { Navigate, useNavigate, useParams } from "react-router-dom";
import MenuItem from "@mui/material/MenuItem";
import { Image } from "react-bootstrap";
import axios from "axios";
import {
  CButton,
  CCol,
  CContainer,
  CFormInput,
  CFormLabel,
  CFormTextarea,
  CRow,
} from "@coreui/react";
import {
  GET_ALL_CATEGORIES,
  GET_ALL_TAGS,
  GET_PRODUCT_ID,
  PUT_EDIT_PRODUCT,
} from "../api/apiService";
import ImageProductEdit from "./ImageProductEdit";
import { Select } from "@mui/material";

const EditProduct = () => {
  const { id } = useParams();
  const [checkUpdate, setCheckUpdate] = useState(false);
  const [productName, setProductName] = useState("");
  const [regularPrice, setRegularPrice] = useState(null);
  const [discountPrice, setDiscountPrice] = useState(null);
  const [productDescription, setProductDescription] = useState("");
  const [quantity, setQuantity] = useState(null);
  const [shortDescription, setShortDescription] = useState("");
  // const [published, setPublished] = useState(true);
  const [note, setNote] = useState("");
  const [categories, setCategories] = useState([]);
  const [categoryAll, setCategoryAll] = useState([]);
  const [tags, setTags] = useState([]);
  const [tagAll, setTagAll] = useState([]);
  const [selectedImages, setSelectedImages] = useState([]);
  const [imageFiles, setImageFiles] = useState([]);
  const [images, setImages] = useState([]);

  useEffect(() => {
    GET_PRODUCT_ID(`Product`, id).then((product) => {
      console.log(product);
      setProductName(product.data.productName);
      setRegularPrice(product.data.buyingPrice);
      setDiscountPrice(product.data.salePrice);
      setProductDescription(product.data.productDescription);
      setQuantity(product.data.quantity);
      setShortDescription(product.data.shortDescription);
      // setPublished(product.data.published);
      setNote(product.data.note);
      setCategories(product.data.productCategoryIds);
      setTags(product.data.productTagIds);
    });
    GET_ALL_CATEGORIES("Categories").then((item) => {
      setCategoryAll(item.data);
    });
    GET_ALL_TAGS("Tag").then((item) => {
      setTagAll(item.data);
    });
    fetchImages();
  }, []);

  const fetchImages = () => {
    axios
      .get(`http://localhost:5284/api/Gallery/by-product/` + id)
      .then((response) => {
        setImages(response.data);
      })
      .catch((error) => {
        console.error("Error fetching images:", error);
      });
  };

  const deleteImageID = (id) => {
    axios
      .delete(`http://localhost:5284/api/Gallery/${id}`)
      .then((response) => {
        if (response.status === 204) {
          // Sau khi xóa thành công, set state để reload dữ liệu ảnh
          fetchImages();
          const updatedImages = images.filter((image) => image.id !== id);
          setImages(updatedImages);
        }
      })
      .catch((error) => {
        console.error("Error deleting image:", error);
      });
  };

  const handleChangeTitle = (e) => {
    setProductName(e.target.value);
  };

  const handleChangePriceRegular = (e) => {
    setRegularPrice(e.target.value);
  };

  const handleChangePriceDiscount = (e) => {
    setDiscountPrice(e.target.value);
  };

  const handleChangeProductDescription = (e) => {
    setProductDescription(e.target.value);
  };

  const handleChangeQuantity = (e) => {
    setQuantity(e.target.value);
  };

  const handleChangeShortDescription = (e) => {
    setShortDescription(e.target.value);
  };

  // const handleChangePublished = (e) => {
  //   setPublished(e.target.value);
  // };

  const handleChangeNote = (e) => {
    setNote(e.target.value);
  };

  const handleChangeCategories = (event) => {
    const selectedIds = event.target.value;
    setCategories(selectedIds);
  };

  const handleChangeTags = (event) => {
    const selectedIds = event.target.value;
    setTags(selectedIds);
  };

  const handleFileChange = (event) => {
    const files = event.target.files;
    const imagesArray = [];
    const filesArray = [];
    for (let i = 0; i < files.length; i++) {
      const file = files[i];
      const reader = new FileReader();
      reader.onloadend = () => {
        imagesArray.push(reader.result);
        if (imagesArray.length === files.length) {
          setSelectedImages([...selectedImages, ...imagesArray]);
          setImageFiles([...imageFiles, ...filesArray]);
        }
      };
      if (file) {
        reader.readAsDataURL(file);
        filesArray.push(file);
      }
    }
  };

  const handleResetImages = () => {
    setSelectedImages([]);
    setImageFiles([]);
  };

  const editProduct = async (e) => {
    e.preventDefault();

    if (
      productName !== "" &&
      regularPrice !== null &&
      discountPrice !== null &&
      categories.length > 0
    ) {
      const salePriceNumber = parseFloat(regularPrice);
      const buyingPriceNumber = parseFloat(discountPrice);

      let product = {
        id: id,
        productName: productName,
        salePrice: salePriceNumber,
        buyingPrice: buyingPriceNumber,
        quantity: quantity,
        shortDescription: shortDescription,
        productDescription: productDescription,
        // published: published,
        note: note,
        productCategoryIds: categories,
        productTagIds: tags,
        productCategoryNames: categories.map(
          (categoryId) =>
            categoryAll.find((category) => category.id === categoryId)
              .categoryName
        ),
        productTagsNames: tags.map(
          (tagId) => tagAll.find((tag) => tag.id === tagId).tagName
        ),
      };
      console.log(product);

      try {
        // Kiểm tra xem người dùng đã chọn ảnh mới hay không
        if (selectedImages.length > 0) {
          // Nếu có ảnh mới, thực hiện cả việc tải lên ảnh và cập nhật thông tin sản phẩm
          const editedProduct = await PUT_EDIT_PRODUCT(
            `Product/${id}`,
            product
          );
          if (editedProduct.status === 204 || editedProduct.status === 200) {
            handleUploadImages(editedProduct.data.id);
          }
        } else {
          // Nếu không có ảnh mới, chỉ cập nhật thông tin sản phẩm
          const editedProduct = await PUT_EDIT_PRODUCT(
            `Product/${id}`,
            product
          );
          if (editedProduct.status === 204 || editedProduct.status === 200) {
            setCheckUpdate(true);
          }
        }
      } catch (error) {
        console.error("Error editing product:", error);
      }
    }
  };

  const handleUploadImages = async (id) => {
    const formData = new FormData();
    formData.append("ProductId", id);
    imageFiles.forEach((image) => {
      formData.append("imageFiles", image);
    });

    try {
      const response = await axios.post(
        `http://localhost:5284/api/Gallery/multiple/`,
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );

      if (response.status === 200) {
        setCheckUpdate(true);
      } else {
        alert("Error occurred while uploading images!");
      }
    } catch (error) {
      console.error("Error:", error);
      alert("Error occurred while uploading images!");
    }
  };

  if (checkUpdate) {
    return <Navigate to="/" />;
  }

  return (
    <div>
      <Grid container spacing={3} style={{ marginBottom: 40 }}>
  <Grid item xs={12}>
    <CContainer
      className="p-3 border rounded"
      style={{ width: 800, margin: "0 auto" }}
    >
      <h3 variant="h4" style={{ textAlign: "center" }}>
        Edit Product
      </h3>
      <Grid container spacing={3}>
        <Grid item xs={4}>
          <Grid container spacing={3}>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Title
              </CFormLabel>
              <CFormInput
                id="title"
                onChange={handleChangeTitle}
                value={productName}
                variant="outlined"
                size="small"
              />
            </Grid>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Product Description
              </CFormLabel>
              <CFormTextarea
                id="productDescription"
                onChange={handleChangeProductDescription}
                multiline
                value={productDescription}
                rows={4}
                variant="outlined"
              />
            </Grid>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Regular Price
              </CFormLabel>
              <CFormInput
                id="regularPrice"
                onChange={handleChangePriceRegular}
                value={regularPrice}
                variant="outlined"
                size="small"
                type="number"
              />
            </Grid>
            <Grid item xs={12}>
              <Typography gutterBottom variant="subtitle1">
                Discount Price
              </Typography>
              <CFormInput
                id="discountPrice"
                onChange={handleChangePriceDiscount}
                value={discountPrice}
                variant="outlined"
                size="small"
                type="number"
              />
            </Grid>
          </Grid>
        </Grid>
        <Grid item xs={4}>
          <Grid container spacing={3}>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Quantity
              </CFormLabel>
              <CFormInput
                id="quantity"
                value={quantity}
                type="number"
                onChange={handleChangeQuantity}
                variant="quantity"
                size="small"
              />
            </Grid>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Short Description
              </CFormLabel>
              <CFormInput
                id="shortDescription"
                value={shortDescription}
                onChange={handleChangeShortDescription}
                variant="outlined"
                size="small"
              />
            </Grid>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Note
              </CFormLabel>
              <CFormInput
                id="note"
                value={note}
                onChange={handleChangeNote}
                variant="outlined"
                size="small"
              />
            </Grid>
          </Grid>
        </Grid>
        <Grid item xs={4}>
          <Grid container spacing={3}>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Tag
              </CFormLabel>
              <TextField
                id="tags"
                name="tags"
                select
                style={{ width: "100%" }}
                value={tags}
                onChange={handleChangeTags}
                SelectProps={{
                  multiple: true,
                }}
                variant="outlined"
              >
                {tagAll.map((option) => (
                  <MenuItem key={option.id} value={option.id}>
                    {option.tagName}
                  </MenuItem>
                ))}
              </TextField>
            </Grid>
            <Grid item xs={12}>
              <CFormLabel gutterBottom variant="subtitle1">
                Category
              </CFormLabel>
              <TextField
                id="categories"
                name="categories"
                select
                style={{ width: "100%" }}
                value={categories}
                onChange={handleChangeCategories}
                SelectProps={{
                  multiple: true,
                }}
                variant="outlined"
              >
                {categoryAll.map((option) => (
                  <MenuItem key={option.id} value={option.id}>
                    {option.categoryName}
                  </MenuItem>
                ))}
              </TextField>
            </Grid>
            <Grid item xs={12}>
              <Typography gutterBottom variant="subtitle1">
                Product Images
              </Typography>
              <CCol xs="3">
                <div style={{ display: "flex", flexWrap: "wrap" }}>
                  {images.map((imageData, index) => (
                    <div
                      key={index}
                      style={{
                        marginRight: "10px",
                        marginBottom: "10px",
                        position: "relative",
                      }}
                    >
                      <img
                        src={`data:image/*;base64,${imageData.image}`}
                        alt={`Existing Image ${index}`}
                        width={120}
                        style={{
                          borderRadius: "5px",
                          boxShadow: "0 0 5px rgba(0, 0, 0, 0.2)",
                        }}
                      />
                      <button
                        className="btn btn-sm btn-danger"
                        style={{
                          position: "absolute",
                          top: "3px",
                          right: "3px",
                        }}
                        onClick={() => deleteImageID(imageData.id)}
                      >
                        Xóa
                      </button>
                    </div>
                  ))}
                </div>
              </CCol>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
      <div className="d-grid gap-2 mt-3">
        <CButton color="primary" onClick={editProduct}>
          Edit
        </CButton>
      </div>
    </CContainer>
  </Grid>
</Grid>

    </div>
  );
};

export default EditProduct;
