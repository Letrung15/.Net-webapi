import React from 'react'
import {
  CCloseButton,
  CNavItem,
  CNavTitle,
  CSidebar,
  CSidebarBrand,
  CSidebarHeader,
  CSidebarNav,
} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { logo } from '../assets/brand/logo'
import { sygnet } from '../assets/brand/sygnet'
import { cilCloudDownload, cilLayers, cilPlus, cilPuzzle, cilSpeedometer, cilTag } from '@coreui/icons'

const AppSidebar = () => {

  return (
    <CSidebar
      className="border-end"
      colorScheme="dark"
      position="fixed"
    >
      <CSidebarHeader className="border-bottom">
        <CSidebarBrand to="/">
        <CIcon customClassName="sidebar-brand-full" icon={logo} height={32} />
          <CIcon customClassName="sidebar-brand-narrow" icon={sygnet} height={32} />
        </CSidebarBrand>
        <CCloseButton
          className="d-lg-none"
          dark
        />
      </CSidebarHeader>
      <CSidebarNav>
      <CSidebar >
        <CSidebarNav>
          <CNavTitle style={{ fontSize: '20px', color: 'white' }}>Vuong Le Trung</CNavTitle>
          <CNavItem href="/"><CIcon customClassName="nav-icon" icon={cilPuzzle} /> Product</CNavItem>
          <CNavItem href="/add/product"><CIcon customClassName="nav-icon" icon={cilPlus} /> Product Add</CNavItem>
          {/* <CNavItem href="#"><CIcon customClassName="nav-icon" icon={cilLayers} /> Category</CNavItem>
          <CNavItem href="#"><CIcon customClassName="nav-icon" icon={cilPlus} /> Category Add</CNavItem>
          <CNavItem href="#"><CIcon customClassName="nav-icon" icon={cilTag} /> Tag</CNavItem>
          <CNavItem href="#"><CIcon customClassName="nav-icon" icon={cilPlus} /> Tag Add</CNavItem> */}
        </CSidebarNav>
      </CSidebar>
      </CSidebarNav>
    </CSidebar>
  )
}

export default React.memo(AppSidebar)
