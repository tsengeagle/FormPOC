Feature: 讀取手術全期表單


Scenario: 依照定義取得空白表單
Given 表單定義
| Name | 
| 表單01 |
Given 欄位定義
| Name | FormName | ColumnType   | DefaultContent |
| 欄位01 | 表單01     | StringColumn | string content |
| 欄位02 | 表單01     | CheckColumn  | check box      |

When 表單名稱："表單01"，按下新增表單

Then 得到表單內容
| Name |
| 表單01 |
Then 得到表單欄位
| Name |  
| 欄位01 | 
| 欄位02 | 
Then 字串欄位："欄位01"
| Name | Content        |
| 欄位01 | string content |
Then 選擇盒子欄位："欄位02"
| Name | Content   |
| 欄位02 | check box |
