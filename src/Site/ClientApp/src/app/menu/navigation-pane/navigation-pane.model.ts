import { Injectable } from '@angular/core';

export interface NavigationItem {
  name: string;
  url: string;
  roles: string[];
}

export interface NavigationGroup {
  name: string;
  items: NavigationItem[];
  roles:string[];
}


export const navigationPaneItems: NavigationGroup[] = [
  {
    name: "Управление",
    items: [
      { name: "Редактор документов", url: "/СustomizeReport", roles: ['Admin'] },
      { name: "Пользователи", url: "/Users", roles: ['Admin'] },
      { name: "Филиалы", url: "/Departments", roles: ['Admin'] },
      { name: "Статусы", url: "/OrderStatuses", roles: ['Admin'] },
      { name: "Типы заказов", url: "/OrderKinds", roles: ['Admin'] },
      { name: "Автодополнение", url: "orders/autocomplete-item", roles: ['Admin'] },
      { name: "Профиль", url: "/Profile", roles: ['Admin', 'Engineer', 'Manager'] }
    ],
    roles: ['Admin', 'Engineer','Manager']
  },
  {
    name: "Финансы",
    items: [
      { name: "Приход/расход", url: "FinancialItemValues", roles: ['Admin'] },
      { name: "Финансовые группы филиалов", url: "FinancialGroupItems", roles: ['Admin'] },
      { name: "Статьи бюджета", url: "FinancialItems", roles: ['Admin']},
      { name: "Вознаграждения", url: "UserInterests", roles: ['Admin'] }
    ],
    roles: ['Admin']
  },
  {
    name: "Складской учет",
    items: [
      { name: "Складские остатки", url: "WarehouseItems", roles: ['Admin'] },
      { name: "Склады", url: "Warehouses", roles: ['Admin'] },
      { name: "Категории товаров", url: "ItemCategories", roles: ['Admin'] },
      { name: "Номенклатура", url: "GoodItems", roles: ['Admin'] },
      { name: "Контрагенты", url: "Contractors", roles: ['Admin'] },
      { name: "Приходные накладные", url: "IncomingDocs", roles: ['Admin'] },
      { name: "Списания", url: "CancellationDocs", roles: ['Admin'] },
      { name: "Перемещения", url: "TransferDocs", roles: ['Admin'] },
    ],
    roles: ['Admin']
  },
  {
    name: "Общие отчеты",
    items: [
      { name: "Работа исполнителей", url: "EngineerWorkReport", roles: ['Admin'] },
      { name: "Ипользованные запчасти", url: "UsedDeviceItemsReport", roles: ['Admin'] },
      { name: "Доход и расход", url: "IncomeAndExpenseReport", roles: ['Admin'] },
      { name: "Вознаграждение", url: "UserInterestReport", roles: ['Admin'] },
      { name: "Приход и расход на складе", url: "WarehouseFlowReport", roles: ['Admin'] }
    ],
    roles: ['Admin']
  }

];
