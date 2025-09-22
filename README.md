# 🚀 โปรแกรมควบคุมการส่งข้อความแบบชุด Inkjet Keyence

![GitHub repo size](https://img.shields.io/github/repo-size/username/repo-name)
![GitHub contributors](https://img.shields.io/github/contributors/username/repo-name)
![GitHub stars](https://img.shields.io/github/stars/username/repo-name?style=social)
![GitHub forks](https://img.shields.io/github/forks/username/repo-name?style=social)

---

## 📖 คำอธิบาย
โปรเจกต์นี้เป็นระบบสำหรับควบคุมเครื่องพิมพ์ Inkjet Keyence MK-G1000 Series  
มีฟีเจอร์หลัก เช่น:  
- แสดงสถานะเครื่องพิมพ์ ทั้งหมด เช่น Ink, Solvent, Pump, Filter เป็นต้น
- ส่งข้อความแบบชุด (Multiple text) โดยการ อัพโหลดไฟล์ CSV เพื่อพิมพ์ตามลําดับข้อความได้
- แสดง Log การเกิด error และ warning ของเครื่อง Inkjet ทั้งหมด เช่น ErrorCode ErrorName Detail Time Shift เป็นต้น 
- แสดง Log การยิงจํานวนข้อความ เช่น ข้อความที่ยิง จํานวนครั้ง วันที่เริ่ม วันที่สิ้นสุด เป็นต้น
- รายการ Log ต่างๆ สามารถ Filter ตามวันที่ได้ และ Export ไฟล์ Excel ได้

สามารถใช้งานผ่าน คอมพิวเตอร์หรือโน้ตบุ๊ก และเชื่อมต่อ Inkjet พร้อมกันได้สูงสุดได้หลายเครื่อง ผ่าน **IP Address**

---

## 📸 ตัวอย่างหน้าจอ (Screenshots)

ภาพรวมการทำงานของระบบ:

![หน้าจอ login](./images/login.png)  
*รูปที่ 1: หน้า login*

![หน้าจอOverview](./images/overview.png)  
*รูปที่ 2: หน้า Overview แสดงรายการ Inkjet ทั้งหมด*

![หน้าจอOverview](./images/overview.png)  
*รูปที่ 3: หน้า Overview แสดงรายการ Inkjet ทั้งหมด*

![หน้าจอCsvMarking](./images/csv.png)  
*รูปที่ 4: หน้า CsvMarking สําหรับส่งข้อความ*

![หน้าจอerror](./images/error.png)  
*รูปที่ 5: หน้า error แสดงรายการ error Inkjet*

![หน้าจอconnection](./images/connection.png)  
*รูปที่ 6: หน้า connection แสดงรายการเชื่อมต่อ Inkjet*

---


## ⚙️ วิธีติดตั้งโปรแกรม

1. นำโฟลเดอร์ `Inkjet` ไว้บน **Desktop**  
2. เข้าไปที่ `Inkjet -> setup`  
3. คลิกโปรแกรม `inkjet.exe` เพื่อทำการติดตั้ง  

---

## 🖥️ วิธีใช้งานโปรแกรมเบื้องต้น

### 1. เพิ่มผู้ใช้งาน
- ไปที่โฟลเดอร์ `data -> User`  
- เพิ่มข้อมูลผู้ใช้งาน:
  - User ID -> 1  
  - UserName -> your name  
  - User Password -> your password  
  - User Role -> your operator name  
  - User Password Operator -> your operator password  

### 2. ล็อคอินเข้าสู่ระบบ
- ใช้ **UserName** และ **User Password**  

### 3. เพิ่มข้อมูล Setting
- **Email Alert:** ใส่อีเมลสำหรับแจ้งเตือนเมื่อเกิด Error / Warning (ใส่หลายอีเมลได้)  
- **Shift Setting:** กรอกชื่อกะและเวลา เช่น กะเช้า 8:00, กะกลางวัน 12:00, กะดึก 21:00  
- **User:** แก้ไขข้อมูล Login และข้อมูล Operator  

### 4. เพิ่มข้อมูล Inkjet
- ไปที่หน้า **Connection**  
- กด **Add**  
- ใส่ **IPAddress** และ **Inkjet Name**  

### 5. หน้า Overview
- แสดงข้อมูลและรายละเอียด Inkjet ทั้งหมด  
- ข้อมูลอัพเดททุก ๆ 5 วินาที  
- **Status:**  
  - สีเทา → Stop, Suspended, Disconnect  
  - สีเขียว → Printable  
  - สีส้ม → Warning  
  - สีแดง → Error  
  - สีเหลือง → Starting, Shutting Down  
- แสดงชื่อเครื่อง, IP Address  
- ตรวจจับสถานะ Ink / Solvent / Pump / Filter จาก **Error Detail**  

### 6. หน้า CSV Marking
- **Upload file:** CSV ต้องมีหัวข้อชื่อ `Detail`  
- เลือก **Inkjet** ที่ต้องการยิง  
- **Program No.:** ระบุ Program ที่ต้องการยิง  
- **Block No.:** ระบุ Block (สร้างใหม่อัตโนมัติหากยังไม่มี)  
- **Current Data:** ข้อมูลที่ยิงไปแล้ว  
- **Next Data:** ข้อมูลที่จะยิงต่อไป  
- **Total Amount:** จำนวนครั้งการยิง (เริ่มต้น/สิ้นสุด)  
- **Start:** เริ่มยิง  
- **Stop:** Suspended ชั่วคราว  
- เมื่อยิงครบจะมีแจ้งเตือน Success  
- Inkjet ที่กำลังยิงไม่สามารถแก้ไขได้  
- Upload file ใหม่ → Clear ข้อมูลเก่า  
- Program No ไม่เกิน 500, Block No ไม่เกิน 256  

### 7. หน้า Error History
- ข้อมูลอัพเดททุก 5 วินาที  
- Error / Warning ของ Inkjet จะถูกเพิ่มในตาราง  
- ค้นหาตาม **เครื่อง, Shift, วันที่**  
- Export ข้อมูลเป็น Excel  

### 8. หน้า Data Log
- ข้อมูลอัพเดททุก 5 วินาที  
- เปลี่ยนสถานะ Inkjet (Stop → Start) → เพิ่ม row ใหม่ (Qty, Date End ว่าง)  
- เปลี่ยนสถานะ Inkjet (Start → Stop) → อัพเดท Qty และ Date End  
- เปลี่ยนโปรแกรม → อัพเดท Qty และ Date End และเพิ่ม row ใหม่  
- ค้นหาตาม **เครื่อง, Shift, วันที่**  
- Export ข้อมูลเป็น Excel  

### 9. หน้า Connection
- แสดงข้อมูล Inkjet ทั้งหมด  
- **Add:** เพิ่ม Inkjet  
- **Edit:** แก้ไข Inkjet  
- **Delete:** ลบ Inkjet  

---

## 🖥️ เครื่องมือที่ใช้ในการพัฒนา

1. ระบบปฏิบัติการ Windows 10  
2. Microsoft Visual Studio 2022
  - Guna.UI2.WinForms 2.0.4.7  
  - Newtonsoft.Json 13.0.3  
3. .NET Framework 4.7.2  
