import React, { useEffect, useState } from "react";
import {
  CAlert,
  CButton,
  CCardBody,
  CCol,
  CRow,
  CTable,
  CTableBody,
  CTableHead,
  CTableDataCell,
  CTableRow,
  CTableHeaderCell,
} from "@coreui/react";
import { Link } from "react-router-dom";
import { DELETE_PRODUCT_ID, GET_ALL_PRODUCTS } from "../api/apiService";
import ImageProduct from "./ImageProduct";
import { CPagination } from "@coreui/react";
import 'bootstrap/dist/css/bootstrap.min.css'
import '@coreui/coreui/dist/css/coreui.min.css'

const Home = () => {
  const [products, setProducts] = useState([]);
  const [checkDeleteProduct, setCheckDeleteProduct] = useState(false);
  const [close, setClose] = useState(false);

  useEffect(() => {
      GET_ALL_PRODUCTS("Product").then((item) => setProducts(item.data));
  }, []);

  //const RawHTML = ({ body, className }) => (
  //    <div
  //        className={className}
  //        dangerouslySetInnerHTML={{ __html: body.replace(/\n/g, "<br />") }}
  //    />
  //);

  const deleteProductID = (id) => {

      DELETE_PRODUCT_ID(`Product/${id}`).then((item) => {
          console.log(item);
          if (item.status === 204) {
              setCheckDeleteProduct(true);
              // setProducts(products.filter((key) => key.idProduct !== id));
              GET_ALL_PRODUCTS("Product").then((item) => setProducts(item.data));
          }
      });
  };
  console.log(products);


  return (
    <CRow>
      <CCol>
        <CCardBody>
        {checkDeleteProduct && (
            <CAlert
              color="success"
              closeButton
              onClick={() => {
                setClose(true);
                setCheckDeleteProduct(false);
            }}
            >
              Delete successfully
            </CAlert>
          )}
            <CTable>
              <CTableHead>
                <CTableRow>
                  <CTableHeaderCell  scope="col">Title</CTableHeaderCell >
                  <CTableHeaderCell  scope="col"style={{Width:50 }}>
                    Image
                  </CTableHeaderCell >
                  <CTableHeaderCell  scope="col">
                  Sale Price
                  </CTableHeaderCell >
                  <CTableHeaderCell  scope="col">
                  Buying Price
                  </CTableHeaderCell >
                  {/* <CTableHeaderCell  scope="col">
                  Quantity
                  </CTableHeaderCell >
                  <CTableHeaderCell  scope="col">
                  Short Description
                  </CTableHeaderCell >
                  <CTableHeaderCell  scope="col">
                  Product Description
                  </CTableHeaderCell >
                  <CTableHeaderCell  scope="col">
                  Published
                  </CTableHeaderCell >
                  <CTableHeaderCell  scope="col">
                  Note
                  </CTableHeaderCell > */}
                  <CTableHeaderCell  scope="col">
                  Category
                  </CTableHeaderCell >
                  <CTableHeaderCell  scope="col">
                    Modify
                  </CTableHeaderCell >
                  <CTableHeaderCell  scope="col">
                    Delete
                  </CTableHeaderCell >
                </CTableRow>
              </CTableHead>
              <CTableBody>
              {products.length > 0 &&
                                        products.map((row) => (
                  <CTableRow key={row.id}>
                    <CTableDataCell width="260" scope="row">
                      {row.productName}
                    </CTableDataCell>
                    <CTableDataCell align="center">
                     <ImageProduct productId={row.id} key={row.id}/>
                    </CTableDataCell>
                    <CTableDataCell align="center">
                    ${row.salePrice}.00
                    </CTableDataCell>
                    <CTableDataCell align="center">
                    ${row.buyingPrice}.00
                    </CTableDataCell>
                    {/* <CTableDataCell align="center">
                    {row.quantity}
                    </CTableDataCell>
                    <CTableDataCell align="center">
                    {row.shortDescription}
                    </CTableDataCell>
                    <CTableDataCell align="center">
                    {row.productDescription}
                    </CTableDataCell>
                    <CTableDataCell align="center">
                    {row.published}
                    </CTableDataCell>
                    <CTableDataCell align="center">
                    {row.note}
                    </CTableDataCell> */}
                    <CTableDataCell width="100" align="center">
                    {row?.productCategoryNames[0]}
                    </CTableDataCell>
                    <CTableDataCell align="center">
                      <Link to={`/edit/product/${row.id}`}>
                        <CButton size="sm" color="primary">
                          Edit
                        </CButton>
                      </Link>
                    </CTableDataCell>
                    <CTableDataCell align="center">
                      <CButton
                        size="sm"
                        color="danger"
                        onClick={() => deleteProductID(row.id)}
                      >
                        Remove
                      </CButton>
                    </CTableDataCell>
                  </CTableRow>
                ))}
              </CTableBody>
            </CTable>          
        </CCardBody>
      </CCol>
    </CRow>
  );
};

export default Home;
