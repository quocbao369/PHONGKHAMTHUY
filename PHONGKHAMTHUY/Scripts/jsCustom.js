var isSidebarCollapsed = false;
// Lắng nghe sự kiện click trên nút bấm
document.getElementById('sidebarToggleBtn').addEventListener('click', function () {
    // Lấy ra phần tử #main-wrapper
    var mainWrapper = document.getElementById('main-wrapper');

    // Kiểm tra và cập nhật trạng thái của sidebar
    if (isSidebarCollapsed) {
        mainWrapper.setAttribute('data-sidebartype', 'full-sidebar');
        document.querySelector('.topbar .top-navbar .navbar-collapse').style.marginLeft = '250px'; // Điều chỉnh margin-left của topbar khi sidebar được mở rộng
        document.querySelector('.page-wrapper').style.marginLeft = '250px'; // Điều chỉnh margin-left của page-wrapper khi sidebar được mở rộng
        isSidebarCollapsed = false;
    } else {
        mainWrapper.setAttribute('data-sidebartype', 'mini-sidebar');
        document.querySelector('.topbar .top-navbar .navbar-collapse').style.marginLeft = '65px'; // Điều chỉnh margin-left của topbar khi sidebar được thu nhỏ
        document.querySelector('.page-wrapper').style.marginLeft = '65px'; // Điều chỉnh margin-left của page-wrapper khi sidebar được thu nhỏ
        isSidebarCollapsed = true;
    }
});








// Script Home

// Lấy nội dung của card và các phần tử liên quan khi nhấn nút in
function printModal1() {
    // Lấy nội dung của card và các phần tử liên quan
    var cardContent = document.querySelector('#modal-1 .card').innerHTML;

    // Tạo một iframe ẩn
    var iframe = document.createElement('iframe');
    iframe.style.height = '0';
    iframe.style.width = '0';
    iframe.style.position = 'absolute';
    document.body.appendChild(iframe);
    var doc = iframe.contentDocument || iframe.contentWindow.document;

    // Thêm nội dung của card vào iframe
    doc.write('<!DOCTYPE html><html><head><title>PHÒNG KHÁM THÚ Y</title>');
    doc.write('<link href="../Public/dist/css/style.min.css" rel="stylesheet">');
    doc.write('<link href="../Public/dist/css/styleCustom.css" rel="stylesheet">');
    doc.write('</head><body>');
    doc.write(cardContent);
    doc.write(' </body></html>');
    doc.close();

    // Gọi phương thức print trên iframe
    iframe.contentWindow.print();
}

function printModal2() {
    // Lấy nội dung của card và các phần tử liên quan
    var cardContent = document.querySelector('#modal-2 .card').innerHTML;


    // Tạo một iframe ẩn
    var iframe = document.createElement('iframe');
    iframe.style.height = '0';
    iframe.style.width = '0';
    iframe.style.position = 'absolute';
    document.body.appendChild(iframe);
    var doc = iframe.contentDocument || iframe.contentWindow.document;

    // Thêm nội dung của card vào iframe
    doc.write('<!DOCTYPE html><html><head><title>PHÒNG KHÁM THÚ Y</title>');
        doc.write('<link href="../Public/dist/css/style.min.css" rel="stylesheet">');
            doc.write('<link href="../Public/dist/css/styleCustom.css" rel="stylesheet">');
                doc.write('</head><body><div class="card">');
                    doc.write(cardContent);
                    doc.write('</div></body></html>');
        doc.close();

        // Gọi phương thức print trên iframe
        iframe.contentWindow.print();
};
function printModal3() {
    // Lấy nội dung của card và các phần tử liên quan
    var cardContent = document.querySelector('#modal-3 .card').innerHTML;


        // Tạo một iframe ẩn
        var iframe = document.createElement('iframe');
        iframe.style.height = '0';
        iframe.style.width = '0';
        iframe.style.position = 'absolute';
        document.body.appendChild(iframe);
        var doc = iframe.contentDocument || iframe.contentWindow.document;

        // Thêm nội dung của card vào iframe
        doc.write('<!DOCTYPE html><html><head><title>PHÒNG KHÁM THÚ Y</title>');
            doc.write('<link href="../Public/dist/css/style.min.css" rel="stylesheet">');
                doc.write('<link href="../Public/dist/css/styleCustom.css" rel="stylesheet">');
                    doc.write('</head><body><div class="card">');
                        doc.write(cardContent);
                        doc.write('</div></body></html>');
            doc.close();

            // Gọi phương thức print trên iframe
            iframe.contentWindow.print();
};




// Scrip LoginAccount
// Hàm này dùng để điền thông tin vào modal
function getInforAccount(accountId) {
    // Hiển thị input readonly
    document.getElementById('tentaikhoan').classList.remove('d-none');
    // Ẩn input có thể chỉnh sửa
    document.getElementById('user').classList.add('d-none');
    // Hiển thị một số thông tin khác
    document.getElementById('MSTTK').classList.remove('d-none');

    // Xóa dữ liệu ban đầu
    $('#ngaytao').text("");
    $('#ngaysua').text("");
    $.ajax({
        url: '/Setting/GetAccountData', 
        data: { accountId: accountId },
        success: function (data) {
            // Điền dữ liệu vào modal khi nhận được phản hồi từ máy chủ
            $('#idtaikhoan').val(data.IDTAIKHOAN);
            $('#nhomnguoidung').val(data.IDNHOMNGUOIDUNG);
            $('#tentaikhoan').val(data.TENDANGNHAP);
            $('#matkhau').val(data.MATKHAU);
            $('#hoten').val(data.HOTEN);
            $('#dienthoai').val(data.DIENTHOAI);
            $('#email').val(data.EMAIL);
            $('#gioitinh').val(data.GIOITINH);
            $('#ngaytao').text(convertTimestampToNormalDate(data.NGAYTHEM));
            $('#ngaysua').text(convertTimestampToNormalDate(data.NGAYSUA));
            if (data.HINHDAIDIEN != null) {
                $('#avatar-preview').attr('src', '/Public/assets/images/avatar/' + data.HINHDAIDIEN);
            } else {
                $('#avatar-preview').attr('src', '/Public/assets/images/avatar.png'); // Đường dẫn của hình ảnh mặc định
            }
        },
        error: function () {
            alert('Đã xảy ra lỗi khi lấy dữ liệu tài khoản.');
        }
    });
}

// lấy thông tin của vật nuôi
// Hàm này dùng để điền thông tin vào modal
function showDataPet(id) {

    // Xóa dữ liệu ban đầu
    $('#ngaytao').text("");
    $('#ngaysua').text("");
    $.ajax({
        url: '/Pet/GetPetInfor',
        data: { id: id },
        success: function (data) {

            // Điền dữ liệu vào modal khi nhận được phản hồi từ máy chủ
            $('#modal-edit #idvatnuoi').val(data.IDVATNUOI);
            $('#modal-edit #tenkhachhang').val(data.TENKHACHHANG);
            $('#modal-edit #tenvn').val(data.TEN);
            $('#modal-edit #ngaysinh').val(chuyenNgay(data.NGAYSINH));
            $('#modal-edit #loai').val(data.LOAI);
            $('#modal-edit #giong').val(data.GIONG);
            $('#modal-edit #gioitinh').val(data.GIOITINH);
            $('#modal-edit #mausac').val(data.MAUSAC);
            $('#modal-edit #cannang').val(data.CANNANG);
            $('#modal-edit #tuoi').val(data.TUOI);
            $('#modal-edit #ngaytao').text(convertTimestampToNormalDate(data.NGAYTAO));
            $('#modal-edit #ngaysua').text(convertTimestampToNormalDate(data.NGAYSUA));
        },
        error: function () {
            alert('Đã xảy ra lỗi khi lấy dữ liệu tài khoản.');
        }
    });
}

// Lấy thuông tin thuốc
function showDataMedicine(id) {

    // Xóa dữ liệu ban đầu
    $('#ngaytao').text("");
    $('#ngaysua').text("");
    $.ajax({
        url: '/Medicine/getInfor',
        data: { id: id },
        success: function (data) {

            // Điền dữ liệu vào modal khi nhận được phản hồi từ máy chủ
            $('#modal-edit #idthuocvt').val(data.IDTHUOCVT);
            $('#modal-edit #tenthuocvt').val(data.TENTHUOCVT);
            $('#modal-edit #iddanhmuc').val(data.IDDANHMUC);
            $('#modal-edit #donvi').val(data.DONVI);
            $('#modal-edit #quycach').val(data.QUYCACH);
            $('#modal-edit #giaban').val(data.GIABAN);
            $('#modal-edit #gianhap').val(data.GIANHAP);
            $('#modal-edit #cachdung').val(data.CACHDUNG);
            $('#modal-edit #soluongtrenngay').val(data.SOLUONGTRENNGAY);
            $('#modal-edit #thanhphan').val(data.THANHPHAN);
            $('#modal-edit #ghichu').val(data.GHICHU);
            $('#modal-edit #hsd').val(chuyenNgay(data.HSD));

            $('#modal-edit #ngaytao').text(convertTimestampToNormalDate(data.NGAYTAO));
            $('#modal-edit #ngaysua').text(convertTimestampToNormalDate(data.NGAYSUA));
        },
        error: function () {
            alert('Đã xảy ra lỗi khi lấy dữ liệu tài khoản.');
        }
    });
}

function showDataLichkham(id) {

    $('#modal-edit #hoten').text("");
    $('#modal-edit #sdt').text("");
    $('#modal-edit #dc').text("");
    $('#modal-edit #ten').text("");
    $('#modal-edit #tuoi').text("");
    $('#modal-edit #loai').text("");
    $.ajax({
        url: '/MedicalExamination/getInfor',
        data: { id: id },
        success: function (data) {

            // Điền dữ liệu vào modal khi nhận được phản hồi từ máy chủ
            $('#modal-edit #idlichkham').val(data.LICHHEN.IDLICHKHAM);
            $('#modal-edit #trangthai').val(data.LICHHEN.TRANGTHAI);
            $('#modal-edit #thoigiankham').val(convertToDatetimeLocal(data.LICHHEN.THOIGIANKHAM));
            $('#modal-edit #lydo').val(data.LICHHEN.LYDO);

            $('#modal-edit #hoten').text("Họ tên: "+data.CUSTOMER.HOTEN);
            $('#modal-edit #sdt').text("SĐT: "+data.CUSTOMER.DIENTHOAI);
            $('#modal-edit #dc').text("Đ/c: "+data.CUSTOMER.DIACHI);
            $('#modal-edit #ten').text("Tên: "+data.PET.TEN);
            $('#modal-edit #tuoi').text("Tuổi: "+data.PET.TUOI);
            $('#modal-edit #loai').text("Loài: "+data.PET.LOAI);            
        },
        error: function () {
            alert('Đã xảy ra lỗi khi lấy dữ liệu tài khoản.');
        }
    });
}



function addUser() {
    // Lấy tham chiếu đến modal có id là "modal-2"
    var modal = document.getElementById('modal-edit');
    // Hiển thị input readonly
    document.getElementById('user').classList.remove('d-none');
    // Ẩn input có thể chỉnh sửa
    document.getElementById('tentaikhoan').classList.add('d-none');
    // Ẩn một số thông tin khác
    document.getElementById('MSTTK').classList.add('d-none');

    var inputField = document.getElementById("user");
    // Thêm thuộc tính required
    inputField.setAttribute("required", "required");

    resetFormFields(modal);
    $('#modal-edit').modal('show');
}

// hàm này dùng đẻ hiển thị ảnh
function showImage() {
    // Lấy input file
    var input = document.getElementById('anhdaidien');

    // Kiểm tra xem người dùng đã chọn hình ảnh chưa
    if (input.files && input.files[0]) {
        // Tạo một FileReader để đọc dữ liệu của hình ảnh
        var reader = new FileReader();

        // Xử lý khi FileReader đã đọc xong dữ liệu của hình ảnh
        reader.onload = function (event) {
            // Gán đường dẫn của hình ảnh cho thuộc tính src của thẻ img
            document.getElementById('avatar-preview').src = event.target.result;
        };

        // Đọc dữ liệu của hình ảnh
        reader.readAsDataURL(input.files[0]);
    }
}
// hàm này dùng
function clickSuscess() {
    var element = document.getElementById('actionModal');
    element.click();
}

// nút thêm mới
function clickAdd() {
    var element = document.getElementById('btnAdd');
    element.click();
}






/**/
/**/
// #### Một số hàm dùng chung ####
// Hàm chuyển đổi timestamp thành ngày tháng bình thường
function convertTimestampToNormalDate(timestampString) {
    var timestamp = parseInt(timestampString.match(/\d+/)[0]); // Lấy phần số nguyên từ chuỗi
    var date = new Date(timestamp); // Tạo đối tượng Date từ timestamp
    return date.toLocaleString(); // Chuyển đổi thành ngày tháng bình thường
}

// hàm đổi định dạng date
function chuyenNgay(data) {
    // Biến đổi dữ liệu ngày sinh từ dạng "/Date(1651338000000)/" sang định dạng Date
    var ngaySinhMilliseconds = parseInt(data.replace(/\/Date\((\d+)\)\//, '$1'));
    var ngaySinhDate = new Date(ngaySinhMilliseconds);

    // Lấy ngày, tháng, năm
    var ngay = ngaySinhDate.getDate();
    var thang = ngaySinhDate.getMonth() + 1; // Tháng bắt đầu từ 0 nên cần cộng thêm 1
    var nam = ngaySinhDate.getFullYear();

    // Định dạng lại chuỗi ngày tháng theo dạng "yyyy-MM-dd"
    var ngaySinhFormatted = nam + '-' + (thang < 10 ? '0' : '') + thang + '-' + (ngay < 10 ? '0' : '') + ngay;

    return ngaySinhFormatted;
}
// Function to convert the timestamp to datetime-local string
function convertToDatetimeLocal(timestamp) {
    const date = new Date(parseInt(timestamp.replace('/Date(', '').replace(')/', ''), 10));
    const pad = (n) => n < 10 ? '0' + n : n;
    return date.getFullYear() + '-' + pad(date.getMonth() + 1) + '-' + pad(date.getDate()) +
        'T' + pad(date.getHours()) + ':' + pad(date.getMinutes());
}

// Reset form
function resetFormFields(modal) {
    // Lấy tất cả các phần tử input, select, textarea trong modal
    var inputElements = modal.querySelectorAll('input, select, textarea');

    // Đặt giá trị của các phần tử input, select và textarea về giá trị mặc định của chúng
    inputElements.forEach(function (element) {
        if (element.tagName.toLowerCase() === 'input') {
            // Xử lý các phần tử input
            if (element.type === 'checkbox' || element.type === 'radio') {
                element.checked = false;
            } else {
                element.value = '';
            }
        } else if (element.tagName.toLowerCase() === 'select') {
            // Xử lý các phần tử select
            element.selectedIndex = 0; // Chọn lại option đầu tiên
        } else {
            // Xử lý các phần tử textarea
            element.value = '';
        }
    });
}

document.addEventListener('DOMContentLoaded', function () {
    // Lấy phần tử h6 bằng ID
    var dateElement = document.getElementById('currentDate');

    // Lấy ngày tháng hiện tại
    var currentDate = new Date();
    var day = String(currentDate.getDate()).padStart(2, '0');
    var month = String(currentDate.getMonth() + 1).padStart(2, '0'); // Tháng bắt đầu từ 0
    var year = currentDate.getFullYear();

    // Định dạng ngày tháng năm
    var formattedDate = 'Ngày ' +day + ' Tháng ' + month + ' Năm ' + year;

    // Điền ngày tháng hiện tại vào thẻ h6
    dateElement.textContent = formattedDate;
});


/**/
/**/



/**/
/**/
// Khách hàng
/*update*/
function updateCS(id) {
    // Hiển thị một số thông tin khác
    document.getElementById('MSTTK').classList.remove('d-none');

    // Xóa dữ liệu ban đầu
    $('#ngaytao').text("");
    $('#ngaysua').text("");
    $.ajax({
        url: '/Customer/GetCustomerData',
        data: { id: id },
        success: function (data) {
            // Điền dữ liệu vào modal khi nhận được phản hồi từ máy chủ
            $('#modal-edit-cs #idkhachhang').val(data.IDKHACHHANG);
            $('#modal-edit-cs #loaikhachhang').val(data.LOAIKHACHHANG);
            $('#modal-edit-cs #hoten').val(data.HOTEN);
            $('#modal-edit-cs #dienthoai').val(data.DIENTHOAI);
            $('#modal-edit-cs #gioitinh').val(data.GIOITINH);
            $('#modal-edit-cs #diachi').val(data.DIACHI);
            $('#modal-edit-cs #ngaytao').text(convertTimestampToNormalDate(data.NGAYTAO));
            $('#modal-edit-cs #ngaysua').text(convertTimestampToNormalDate(data.NGAYSUA));
        },
        error: function () {
            alert('Đã xảy ra lỗi khi lấy dữ liệu tài khoản.');
        }
    });
}

// Thêm mới trong Phiếu chỉ định
function addNewField() {
    var container = document.getElementById('dynamic-fields-container');
    var count = container.children.length + 1;

    var newField = `
        <div class="row mt-2">
            <div class="col-12">
                <div class="form-group row">
                    <h6 class="ml-3 mt-3">${count}.</h6>
                    <div class="col">
                        <input class="form-control border-right border-top-0 border-right-0 border-left-0"
                               type="text" id="chuandoan${count}" maxlength="500" name="CHUANDOAN" required>
                    </div>
                </div>
            </div>
        </div>
    `;

    container.insertAdjacentHTML('beforeend', newField);
}


function copyText(iconElement) {
    // Lấy phần tử cha của biểu tượng sao chép (đó là phần tử td)
    var tdElement = iconElement.parentNode;

    // Lấy nội dung text của phần tử td
    var textToCopy = tdElement.textContent.trim();

    // Tạo một textarea ẩn để sao chép nội dung
    var tempInput = document.createElement("textarea");
    tempInput.style = "position: absolute; left: -1000px; top: -1000px";
    tempInput.value = textToCopy;
    document.body.appendChild(tempInput);

    // Chọn và sao chép nội dung vào clipboard
    tempInput.select();
    document.execCommand("copy");
    iconElement.className = "fas fa-check";
    setTimeout(function () {
        iconElement.className = "far fa-copy";
    }, 2000);
    // Xóa textarea tạm thời
    document.body.removeChild(tempInput);

}

// check điều kiện nhập
document.getElementById('dienthoai').addEventListener('input', function () {
    if (this.value.length > 10) {
        this.value = this.value.slice(0, 10);
    }
});

// DATATABLE