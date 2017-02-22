Feature: 產生空白表單


Scenario: 依據定義產生表單
Given 表單定義
| Name   |
| Form01 |
| Form02 |
Given 欄位定義
| Name  | FormName | DefaultValue |
| Col01 | Form01   | test 1       |
| Col02 | Form01   | test 2       |
| Col01 | Form02   | ttttt        |

When 取得表單："Form01"

Then 得到表單："Form01"，欄位：
| Name  | Value  |
| Col01 | test 1 |
| Col02 | test 2 |
