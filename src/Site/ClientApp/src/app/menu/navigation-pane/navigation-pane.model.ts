import { Injectable } from '@angular/core';

export interface NavigationItem {
  name: string;
  url:string;
}

export interface NavigationGroup {
  name: string;
  items: NavigationItem[];
}


export const navigationPaneItems: NavigationGroup[] = [
  {
    name: "Управление",
    items: [
      { name: "Редактор документов", url: "/СustomizeReport" },
      { name: "Пользователи", url: "/Users" },
      { name: "Филиалы", url: "/Departments" },
      { name: "Статусы", url: "/OrderStatuses" },
      { name: "Типы заказов", url: "/OrderKinds" },
      { name: "Автодополнение", url: "/AutocompleteItems" },
      { name: "Профиль", url: "/Profile" }
    ],

  },
  {
    name: "Финансы",
    items: [
      { name: "Приход/расход", url: "FinancialItemValues" },
      { name: "Финансовые группы филиалов", url: "FinancialGroupItems" },
      { name: "Статьи бюджета", url: "FinancialItems" },
      { name: "Вознаграждения", url: "UserInterests" }
    ],

  },
  {
    name: "Складской учет",
    items: [
      { name: "Складские остатки", url: "WarehouseItems" },
      { name: "Склады", url: "Warehouses" },
      { name: "Категории товаров", url: "ItemCategories" },
      { name: "Номенклатура", url: "GoodItems" },
      { name: "Контрагенты", url: "Contractors" },
      { name: "Приходные накладные", url: "IncomingDocs" },
      { name: "Списания", url: "CancellationDocs" },
      { name: "Перемещения", url: "TransferDocs" },
    ],
  },
  {
    name: "Общие отчеты",
    items: [
      { name: "Работа исполнителей", url: "EngineerWorkReport" },
      { name: "Ипользованные запчасти", url: "UsedDeviceItemsReport" },
      { name: "Доход и расход", url: "IncomeAndExpenseReport" },
      { name: "Вознаграждение", url: "UserInterestReport" },
      { name: "Приход и расход на складе", url: "WarehouseFlowReport" }
      ]
  }

];
