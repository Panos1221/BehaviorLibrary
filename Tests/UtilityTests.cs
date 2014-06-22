﻿//
//  UtilityTests.cs
//
//  Author:
//       Thomas H. Jonell <@Net_Gnome>
//
//  Copyright (c) 2014 Thomas H. Jonell
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CSTester;
using CSLogging;

using BehaviorLibrary;
using BehaviorLibrary.Components;
using BehaviorLibrary.Components.Utility;
using BehaviorLibrary.Components.Actions;

namespace Tests
{
    [TestCase]
    class UtilityTests
    {

        private CSLogger _log = CSLogger.Instance;

        [BuildUp]
        public void buildup()
        {
            _log.setEnableLogging(true);
            _log.setEnableDebug(true);
            _log.setEnableError(true);
            _log.setEnableMessage(true);
            _log.loadLog("", "utility_tests.log");
            _log.enterScope();
            _log.logMessage("----------------- STARTING BEHAVIOR LIBRARY UTILITY TESTS -----------------");
        }

        [TearDown]
        public void teardown()
        {
            _log.enterScope();
            _log.exitScope();
            _log.logMessage("----------------- ENDING BEHAVIOR LIBRARY UTILITY TESTS -----------------");
            _log.exitScope();
            _log.closeLog();
        }

		[Test]
		public void test_vector(){
			_log.enterScope ();

			UtilityVector vec1 = new UtilityVector (0, 1, 2, 3, 4, 5);

			float mag = vec1.magnitude;
			_log.logDebug ("mag: " + mag);
			VerificationPoint.VerifyTrue ("verify mag gte 0", true, mag >= 0);

			VerificationPoint.VerifyTrue ("norm is not null", true, vec1.normalize () != null);

			UtilityVector vec2 = new UtilityVector (5, 4, 3, 2, 1, 0);

			float dot = vec1.dot (vec2);
			_log.logDebug ("dot: " + dot);
			VerificationPoint.VerifyTrue ("dot between 1 and -1", true, (dot <= 1) && (dot >= -1)); 

			dot = vec1.dot (vec1);
			_log.logError ("self dot: " + dot);
			VerificationPoint.VerifyTrue ("dot with itself should be 1", true, dot == 1);


			_log.exitScope ();
		}

        [Test]
        public void test_1()
        {
            _log.enterScope();

            UtilityVector vector = new UtilityVector(0, 1, 0, 2);
            BehaviorAction action = new BehaviorAction(delegate() { return BehaviorReturnCode.Success; });
            UtilityPair pair = new UtilityPair(vector, action);
            UtilitySelector sel = new UtilitySelector(pair);

            var result = sel.Behave();

            VerificationPoint.VerifyNotEquals("test_case_1", true, result, BehaviorReturnCode.Failure);

            _log.exitScope();
        }
    }
}
